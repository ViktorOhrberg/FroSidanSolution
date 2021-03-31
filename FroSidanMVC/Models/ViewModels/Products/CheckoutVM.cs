using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models.ViewModels.Products
{
    public class CheckoutVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public SummaryVM[] OrderCart { get; set; }
    }
}
