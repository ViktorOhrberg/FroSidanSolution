using FroSidanMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models.ViewModels.Members
{
    public class MyPagesVM
    {
        public string Username { get; set; }
        public Order[] Orders { get; set; }
    }
}
