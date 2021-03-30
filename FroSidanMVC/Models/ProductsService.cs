using FroSidanMVC.Models.Entities;
using FroSidanMVC.Models.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models
{
    public class ProductsService
    {
        public List<Product> shoppingCart; 
        readonly FrosidanContext context;

        public ProductsService(FrosidanContext context)
        {
            this.context = context;
            shoppingCart = new List<Product>();
        }

        public bool AddToCart(int id)
        {
            Product p = GetProductByID(id);
            if (p.Balance > 0)
            {
                shoppingCart.Add(p);
                return true;
            }
            else
                return false;
        }

        public Product GetProductByID(int id)
        {
            return context.Products
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemoveFromCart(int id)
        {
            var q = shoppingCart
                .FirstOrDefault(p => p.Id == id);
            shoppingCart.Remove(q);
        }
        public int QuantityInCart(int id)
        {
            return shoppingCart
                .Where(p => p.Id == id)
                .Count();
        }
        public DetailVM GetSeedById(int id)
        {

            var q = context.Products
                .Where(x => x.Id == id)
                .FirstOrDefault();
            var seed = context.Products
                .Select(x => new DetailVM
                {
                    ProductName = q.Name,
                    Price = Convert.ToInt32(q.Price),
                    Description = q.Description,
                    Url = q.ImgRef,
                }).First();

            return seed;
        }
    }
}
