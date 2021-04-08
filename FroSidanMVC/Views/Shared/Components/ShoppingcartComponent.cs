using FroSidanMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IViewComponentResult> InvokeAsync(List<int> shoppingCart)
        {
            int items;
            decimal price;
            if (shoppingCart == null)
            {
                items = service.GetShoppingCart().Count;
                price = await service.GetOrderPriceAsync() ?? 0;
            }
            else
            {
                items = service.GetShoppingCart(JsonConvert.SerializeObject(shoppingCart)).Count;
                price = await service.GetOrderPriceAsync(shoppingCart) ?? 0;
            }

            //var model = JsonConvert.SerializeObject(new ShoppingCartComponentVM { NoOfItems = pService.GetShoppingCart().Count, TotPrice = await pService.GetOrderPriceAsync() ?? 0 });
            var model = new ShoppingCartComponentVM { NoOfItems = items, TotPrice = price };

            return View("default", model);
        }
    }
    public class ShoppingCartComponentVM
    {
        public int NoOfItems { get; set; }
        public decimal TotPrice { get; set; }
    }
}
