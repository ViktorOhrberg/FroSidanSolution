using System;
using System.Collections.Generic;

#nullable disable

namespace FroSidanMVC.Models.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public string Cart { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? TotPrice { get; set; }

        public virtual AspNetUser Customer { get; set; }
    }
}
