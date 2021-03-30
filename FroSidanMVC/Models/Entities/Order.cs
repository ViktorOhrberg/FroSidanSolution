using System;
using System.Collections.Generic;

#nullable disable

namespace FroSidanMVC.Models.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public string Innehåll { get; set; }
        public DateTime Datum { get; set; }
        public string Leveransadress { get; set; }
        public string KundId { get; set; }
        public string Betalningsmetod { get; set; }

        public virtual AspNetUser Kund { get; set; }
    }
}
