using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Models
{
    public class MembersService
    {
        UserManager<MyIdentityUser> userManager;
        SignInManager<MyIdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public MembersService(
            UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task CreateUser(CreateVM createVm)
        {
            var result = await userManager.CreateAsync(
                new MyIdentityUser { FirstName = createVm.FirstName, LastName = createVm.LastName, UserName = createVm.Email, Email = createVm.Email }, createVm.Password);
        }

        
    }
}
