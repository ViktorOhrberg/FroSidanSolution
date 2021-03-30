using FroSidanMVC.Models;
using FroSidanMVC.Models.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Controls
{
    public class ProductsController : Controller
    {
        private readonly ProductsService pService;

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
        public IActionResult Detail(int id)
        {
            var product = pService.GetProductDetailVM(id);
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

        public IActionResult AddToCart(int id)
        {
            bool q = pService.AddToCart(id);
            return Content(q.ToString());
            //return Content("true");
        }


    }
}
