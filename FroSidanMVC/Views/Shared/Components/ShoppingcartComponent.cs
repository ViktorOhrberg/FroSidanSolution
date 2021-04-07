using FroSidanMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Views.Shared.Components
{
    public class ShoppingcartComponent : ViewComponent
    {
        private readonly ProductsService service;

        public ShoppingcartComponent(ProductsService service)
        {
            this.service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ShoppingCartComponentVM input = new ShoppingCartComponentVM { NoOfItems = service.GetShoppingCart().Count, TotPrice = await service.GetOrderPriceAsync()??0 };
            return View(input);
        }
    }
    public class ShoppingCartComponentVM
    {
        public int NoOfItems { get; set; }
        public decimal TotPrice { get; set; }
    }
}
