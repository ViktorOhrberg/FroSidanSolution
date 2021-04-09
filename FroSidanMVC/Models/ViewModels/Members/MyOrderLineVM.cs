using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models.ViewModels.Members
{
    public class MyOrderLineVM
    {
        public int OrderId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? TempPrice { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
