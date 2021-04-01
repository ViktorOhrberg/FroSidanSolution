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
        [Route("Shop/{category}/{subcategory}")]
        [Route("Shop/{category}")]
        [Route("Shop")]
        [Route("products/shop")]
        [HttpGet]
        public IActionResult Shop(string category, string subcategory)
        {
            if(subcategory == null && category == null)
            {
                var allProducts = pService.GetAllProducts();
                return View(allProducts);
            }
            else if(subcategory == null)
            {
                var productsByCategory = pService.GetProductsByCategory(category);
                return View(productsByCategory);

            }
            else
            {
                var productsBySubCategory = pService.GetProductsBySubCategory(subcategory);
                return View(productsBySubCategory);
            }

            
            //if (filter == "price")
            //{
            //    ShopVM[] sortedProducts = pService.GetSortedByPrice();
            //    return View(sortedProducts);
            //}
            //else if (filter == "name")
            //{
            //    ShopVM[] sortedProducts = pService.GetSortedByName();
            //    return View(sortedProducts);
            //}
        }
        [HttpGet]
        [Route("AddToCart/{id}")]

        public async Task<IActionResult> AddToCartAsync(int id)
        {
            var shoppingCart = await pService.AddToCartAsync(id);
            var model = await pService.GetSummaryVMAsync(shoppingCart);
            return PartialView("_OrderSummary", model);
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

        [HttpGet]
        [Route("Summary")]

        public async Task<IActionResult> Summary()
        {
            SummaryVM[] input = await pService.GetSummaryVMAsync();

            return View(input);
        }

        [Route("Checkout")]
        [Route("CheckoutAsync")]
        [HttpGet]
        public async Task<IActionResult> CheckoutAsync()
        {
            var User = await mService.GetUser();
            CheckoutVM input;

            if (User != null)
            {
                input = new CheckoutVM
                {
                    Name = $"{User.FirstName} {User.LastName}",
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
        [Route("NotLoggedIn")]
        [HttpGet]
        public async Task<IActionResult> NotLoggedInAsync()
        {
            var q = await mService.GetUser();
            if (q == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Checkout");
            }
        }

        [HttpGet]
        [Route("Search/{searchStr}")]

        public async Task<IActionResult> Search(string searchStr)
        {

            var products = pService.GetAllProducts();
            List<ShopVM> temp = new List<ShopVM>();
            var words = searchStr.Split(' ');


            foreach (string search in words)
            {
                var q = products
                    .Where(x => x.Name.Contains(search))
                    .ToList();
                temp.AddRange(q);
            }
            foreach (string search in words)
            {
                var q = products
                    .Where(x => x.SubCategory.Contains(search))
                    .ToList();
                temp = temp.Union(q).ToList();
            }

            var model = temp.ToArray();

            return PartialView("_ShopProducts", model);
        }
    }
}
