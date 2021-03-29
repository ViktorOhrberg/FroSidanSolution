using FroSidanMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Controls
{
    public class ProductsController : Controller
    {
        private readonly ProductsService service;

        public ProductsController(ProductsService service)
        {
            this.service = service;
        }

        [Route("")]
        [Route("products/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
