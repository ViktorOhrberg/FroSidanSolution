using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models
{
    public class ProductsService
    {
        List<int> shoppingCart; // Ska vara av typen product


        

        public ProductsService(List<int> shoppingCart)
        {
            this.shoppingCart = shoppingCart;

        }

        public void AddToCart( /*ProduktId*/ )
        {
            shoppingCart.Add(1);
        }
        public void RemoveFromCart( /*ProduktId*/ )
        {
            //shoppingCart
            //    .FirstOrDefault(/*p => p.ID == ProductID*/)
            //    .Remove();
        }
        public int QuantityInCart( /*ProduktId*/ )
        {
            int quantity = 1;
            //shoppingCart
            //    .Where(p => p.ID == ProduktId)
            //    .Count();
            return quantity;
        }
    }
}
