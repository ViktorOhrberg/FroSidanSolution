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

        [Route("products/shop")]
        [HttpGet]
        public IActionResult Shop()
        {
            ShopVM[] input =
            {
                new ShopVM{Id = 1, Name="A"},
                new ShopVM{Id = 2, Name="B"},
                new ShopVM{Id = 3, Name="C"},
                new ShopVM{Id = 4, Name="D"}
            };
            return View(input);
        }
        [HttpGet]
        [Route("AddToCart/{id}")]

        public async Task<IActionResult> AddToCartAsync(int id)
        {
            bool q = await pService.AddToCartAsync(id);

            return Content(pService.GetShoppingCart().Count().ToString()); // Vad är en lämplig return???
        }
    }
}
