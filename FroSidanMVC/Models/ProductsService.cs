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
        List<Product> shoppingCart; // Ska vara av typen product
        readonly FrosidanContext context;

        public ProductsService(FrosidanContext context)
        {
            this.context = context;
        }

        public void AddToCart(int id)
        {
            shoppingCart.Add(GetProductByID(id));
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
