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

namespace FroSidanMVC.Controls
{
    public class ProductsController : Controller
    {
        public ProductsService pService;

        public ProductsController(ProductsService pService)
        {
            this.pService = pService;
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
        public IActionResult Shop()
        {
            var products = pService.GetAllProducts();
            
            return View(products);
        }
        [HttpGet]
        [Route("AddToCart/{id}")]

        public async Task<IActionResult> AddToCartAsync(int id)
        {
            bool q = await pService.AddToCartAsync(id);

            return Content(pService.QuantityInCart(id).ToString()); // Vad är en lämplig return???
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

        public IActionResult RemoveSingleFromCart(int id)
        {
            pService.RemoveSingleFromCart(id);
            return Content(pService.QuantityInCart(id).ToString());
        }

        [HttpGet]
        [Route("RemoveAllFromCart/{id}")]

        public IActionResult RemoveAllFromCart(int id)
        {
            pService.RemoveAllFromCart(id);
            return Content(pService.QuantityInCart(id).ToString());
        }

        [HttpGet]
        [Route("Summary")]

        public async Task<IActionResult> Summary()
        {
            SummaryVM[] input = await pService.GetSummaryVMAsync();

            return View(input);
        }

        [Route("Checkout")]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
