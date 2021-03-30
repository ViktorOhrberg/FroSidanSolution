using FroSidanMVC.Models.Entities;
using FroSidanMVC.Models.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddToCartAsync(int id)
        {
            Product p = await GetProductByIDAsync(id);
            if (p.Balance > 0)
            {
                shoppingCart.Add(p);
                return true;
            }
            else
                return false;
        }

        public async Task<Product> GetProductByIDAsync(int id)
        {
            return await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
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
        public async Task<DetailVM> GetProductDetailVMAsync(int id)
        {

            var q = await GetProductByIDAsync(id);
            var product = await context.Products
                .Select(x => new DetailVM
                {
                    ProductName = q.Name,
                    Price = Convert.ToInt32(q.Price),
                    Description = q.Description,
                    Url = q.ImgRef,
                }).FirstOrDefaultAsync();

            return product;
            
        }
    }
}
