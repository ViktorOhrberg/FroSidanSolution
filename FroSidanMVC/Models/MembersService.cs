using FroSidanMVC.Models.Entities;
using FroSidanMVC.Models.ViewModels.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models
{
    public class MembersService
    {
        readonly MyContext context;
        UserManager<MyIdentityUser> userManager;
        SignInManager<MyIdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        private readonly IHttpContextAccessor accessor;

        public MembersService(
            UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor accessor,
            MyContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.accessor = accessor;
            this.context = context;
        }

        internal async Task<Order[]> GetOrdersAsync()
        {
            var user = accessor.HttpContext.User.Identity;
            var id = context.AspNetUsers
                .Where(x => x.UserName == user.Name)
                .Select(x => x.Id).Single();
            var orders = context.Orders.Where(x => x.CustomerId == id).ToArray();
            return orders;
                
            //new MyPagesVM { Orders = orders, Username = user.Name };
          
            
            
        }

        public async Task<bool> TryRegisterAsync(MemberRegisterVM memberRegisterVm)
        {
            var result = await userManager.CreateAsync(
                new MyIdentityUser
                { FirstName = memberRegisterVm.FirstName, LastName = memberRegisterVm.LastName, UserName = memberRegisterVm.Email, Email = memberRegisterVm.Email, City = memberRegisterVm.City, Zip = memberRegisterVm.Zip, Street = memberRegisterVm.Street }, memberRegisterVm.Password);
            return result.Succeeded;
        }

        public async Task<bool> TryLoginAsync(MembersLoginVM viewModel)
        {
            var result = await signInManager.PasswordSignInAsync(
                viewModel.Username, viewModel.Password, false, false);

            return result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        internal async Task<MyIdentityUser> GetUser()
        {
            var user = await userManager.GetUserAsync(accessor.HttpContext.User);
            return user;
        }
    }
}
