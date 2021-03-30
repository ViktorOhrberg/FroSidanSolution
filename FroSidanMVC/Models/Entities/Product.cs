using System;
using System.Collections.Generic;

#nullable disable

namespace FroSidanMVC.Models.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? TempPrice { get; set; }
        public int Balance { get; set; }
        public string ImgRef { get; set; }
        public string ThumbRef { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int Quantity { get; set; }
    }
}
