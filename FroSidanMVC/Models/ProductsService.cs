using FroSidanMVC.Models.Entities;
using FroSidanMVC.Models.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FroSidanMVC.Models
{
    public class ProductsService
    {
        readonly MyContext context;
        private readonly IHttpContextAccessor accessor;

        public ProductsService(MyContext context, IHttpContextAccessor accessor)
        {
            this.context = context;
            this.accessor = accessor;
        }

        public async Task<List<int>> AddToCartAsync(int id)
        {

            var shoppingCart = GetShoppingCart();

            Product p = await GetProductByIDAsync(id);
            if (p.Balance > 0)
            {
                shoppingCart.Add(id);
                AddToShoppingCartCookie(shoppingCart);
                return shoppingCart;
            }
            else
                return null;
        }

        internal ShopVM[] GetAllProducts()
        {
            var products = context.Products
                .Select(x => new ShopVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    SubCategory = x.SubCategory,
                    Price = x.Price,
                    ThumbnailUrl = x.ThumbRef,
                    Description = x.Description,
                    Category = x.Category
                })
                .ToArray();

            return products;
        }

        internal ShopVM[] GetProductsByCategory(string category)
        {
            var q = GetAllProducts();
            var qq = q.Where(q => q.Category.ToLower() == category.ToLower())
                .ToArray();
            return qq;
        }

        internal ShopVM[] GetProductsBySubCategory(string subcategory)
        {
            var q = GetAllProducts();
            var qq = q.Where(q => q.SubCategory.ToLower() == subcategory.ToLower())
                .ToArray();
            return qq;
        }

        internal ShopVM[] GetSortedByName()
        {
            var all = GetAllProducts();
            var sorted = all.OrderBy(x => x.Name).ToArray();
            return sorted;
        }

        internal ShopVM[] GetSortedByPrice()
        {
            var all = GetAllProducts();
            var sorted = all.OrderBy(x => x.Price).ToArray();
            return sorted;

        }

        private void AddToShoppingCartCookie(List<int> shoppingCart)
        {
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
            };
            string shoppingCartJson = JsonConvert.SerializeObject(shoppingCart);
            accessor.HttpContext.Response.Cookies.Append("shoppingCart", shoppingCartJson, option);


        }
        public void DeleteCartCookie()
        {
            var shoppingCart = new List<int>();
            accessor.HttpContext.Response.Cookies.Delete("shoppingCart");
        }

        internal async Task<SummaryVM[]> GetSummaryVMAsync()
        {
            return await GetSummaryVMAsync(GetShoppingCart());
        }

        internal async Task<SummaryVM[]> GetSummaryVMAsync(List<int> shoppingCart)
        {
            var q = shoppingCart
                .OrderBy(p => p)
                .ToArray();

            List<SummaryVM> myList = new List<SummaryVM>();
            int LastID = 0;
            SummaryVM tempSum = new SummaryVM();

            foreach (int item in q)
            {
                if (item != LastID)
                {
                    Product tempProd = await GetProductByIDAsync(item);
                    tempSum = new SummaryVM { Id = tempProd.Id, Name = tempProd.Name, Price = tempProd.Price, TempPrice = tempProd.TempPrice, Quantity = 1 };
                    LastID = tempSum.Id;
                    myList.Add(tempSum);
                }
                else
                {
                    tempSum.Quantity++;
                }
            };
            return myList.ToArray();
        }



        public List<int> GetShoppingCart()
        {
            var q = accessor.HttpContext.Request.Cookies["shoppingCart"];

            if (q == null)
            {
                return new List<int>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<int>>(q);
            }
        }

        public async Task<Product> GetProductByIDAsync(int id)
        {
            return await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public List<int> RemoveSingleFromCart(int id)
        {
            if (QuantityInCart(id) > 0)
            {
                var shoppingCart = GetShoppingCart();
                shoppingCart.Remove(id);
                DeleteCartCookie();
                AddToShoppingCartCookie(shoppingCart);
                return shoppingCart;
            }
            else
                return null;
        }

        internal void PlaceOrder(CheckoutVM input)
        {
            Order myOrder = new Order
            {
                CustomerId = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Street = input.Street,
                Zip = input.Zip,
                City = input.City,
                Date = DateTime.Now,
                Cart = accessor.HttpContext.Request.Cookies["shoppingCart"]
            };

            context.Orders.Add(myOrder);
            context.SaveChanges();

        }

        public List<int> RemoveAllFromCart(int id)
        {
            var shoppingCart = GetShoppingCart();
            var q = QuantityInCart(id);
            for (int i = 0; i < q; i++)
            {
                shoppingCart.Remove(id);
            }
            AddToShoppingCartCookie(shoppingCart);
            return shoppingCart;
        }
        public void DeleteCart()
        {
            var shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            AddToShoppingCartCookie(shoppingCart);
        }
        public int QuantityInCart(int id)
        {
            var shoppingCart = GetShoppingCart();
            return shoppingCart
                .Where(p => p == id)
                .Count();
        }
        public async Task<DetailVM> GetProductDetailVMAsync(int id)
        {

            var q = await GetProductByIDAsync(id);
            var product = await context.Products
                .Select(x => new DetailVM
                {
                    ProductName = q.Name,
                    Price = q.Price,
                    Description = q.Description,
                    Url = q.ImgRef,
                }).FirstOrDefaultAsync();
            return product;
        }
    }
}
