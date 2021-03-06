using FroSidanMVC.Models;
using FroSidanMVC.Models.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using FroSidanMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using FroSidanMVC.Views.Shared.Components;
using Newtonsoft.Json;

namespace FroSidanMVC.Controls
{
    public class ProductsController : Controller
    {
        public ProductsService pService;
        private readonly MembersService mService;

        public ProductsController(ProductsService pService, MembersService mService)
        {
            this.pService = pService;
            this.mService = mService;
        }

        [Route("")]
        [Route("products/index")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("products/{id}")]
        public async Task<IActionResult> DetailAsync(int id)
        {
            var product = await pService.GetProductDetailVMAsync(id);
            return View(product);
        }

        [Route("Shop")]
        [Route("products/shop")]
        [HttpGet]
        public IActionResult Shop(string category, string subcategory, string sortBy)
        {
            if (subcategory == null && category == null)
            {
                var allProducts = pService.GetAllProductsAndSort(sortBy);
                return View(allProducts);
            }
            else if (subcategory == null)
            {
                var productsByCategory = pService.GetProductsByCategory(category, sortBy);
                return View(productsByCategory);
            }
            else
            {
                var productsBySubCategory = pService.GetProductsBySubCategory(subcategory, sortBy);
                return View(productsBySubCategory);
            }
        }
        [HttpGet]
        [Route("AddToCartCheckout/{id}")]

        public async Task<IActionResult> AddToCartCheckoutAsync(int id)
        {
            var shoppingCart = await pService.AddToCartAsync(id);
            var model = await pService.GetSummaryVMAsync(shoppingCart);
            return PartialView("_OrderSummary", model);
        }

        [HttpGet]
        [Route("AddToCartShop/{id}")]

        public async Task<IActionResult> AddToCartShopAsync(int id)
        {
            var shoppingCart = await pService.AddToCartAsync(id);
            return ViewComponent("ShoppingcartComponent", shoppingCart);
        }

        [HttpGet]
        [Route("ClearCart")]

        public IActionResult ClearCart()
        {
            pService.DeleteCart();
            return Content(pService.GetShoppingCart().Count().ToString()); // Vad är en lämplig return???
        }
        [HttpGet]
        [Route("RemoveSingleFromCart/{id}")]

        public async Task<IActionResult> RemoveSingleFromCartAsync(int id)
        {

            var shoppingCart = pService.RemoveSingleFromCart(id);

            var model = await pService.GetSummaryVMAsync(shoppingCart);

            return PartialView("_OrderSummary", model);
        }

        [HttpGet]
        [Route("RemoveAllFromCart/{id}")]

        public async Task<IActionResult> RemoveAllFromCartAsync(int id)
        {
            var shoppingCart = pService.RemoveAllFromCart(id);
            var model = await pService.GetSummaryVMAsync(shoppingCart);
            return PartialView("_OrderSummary", model);
        }

        [Route("Checkout")]
        [HttpGet]
        public async Task<IActionResult> CheckoutAsync()
        {
            var User = await mService.GetUser();
            CheckoutVM input;

            if (User != null)
            {
                input = new CheckoutVM
                {
                    Id = User.Id,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Email = User.Email,
                    Street = User.Street,
                    Zip = User.Zip,
                    City = User.City,
                    OrderCart = await pService.GetSummaryVMAsync()
                };
            }
            else
            {
                input = new CheckoutVM
                {
                    OrderCart = await pService.GetSummaryVMAsync()
                };
            };

            return View(input);
        }
        [Route("Checkout")]
        [HttpPost]
        public async Task<IActionResult> CheckoutAsync(CheckoutVM input)
        {
            int orderNum = await pService.PlaceOrderAsync(input);
            pService.DeleteCart();
            TempData["Message"] = $"{orderNum}";


            return RedirectToAction(nameof(CheckoutConfirmed));
        }
        [Route("CheckoutConfirmed")]
        [HttpGet]
        public IActionResult CheckoutConfirmed()
        {
            string ID = ((string)TempData["Message"]);
            CheckoutConfirmedVM input = new CheckoutConfirmedVM
            {
                OrderID = ID
            };
            return View(input);
        }

        [HttpGet]
        [Route("Search/{searchStr}")]

        public IActionResult Search(string searchStr)
        {

            var products = pService.GetAllProducts();
            var words = searchStr.Split(' ');

            foreach (string search in words)
            {
                products = products
                    .Where(x => x.Name.ToLower().Contains(search.ToLower()) || x.SubCategory.ToLower().Contains(search.ToLower()))
                    .ToArray();
            }

            return PartialView("_ShopProducts", products);
        }
    }
}
