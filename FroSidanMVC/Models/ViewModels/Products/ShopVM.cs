using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models.ViewModels.Products
{
    public class ShopVM
    {
        public int Id { get; set; }
        public string SubCategory { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string ThumbnailUrl { get; set; }

       


    }
}
