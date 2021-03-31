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
        public string DeliveryAdress { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMethod { get; set; }

        public virtual AspNetUser Customer { get; set; }
    }
}
