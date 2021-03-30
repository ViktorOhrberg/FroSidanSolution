using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models.ViewModels.Products
{
    public class SummaryVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TempPrice { get; set; }
    }
}
