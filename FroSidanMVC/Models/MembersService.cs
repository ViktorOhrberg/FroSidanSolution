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


        public async Task<bool> TryRegisterAsync(MemberRegisterVM memberRegisterVm)
        {
            var result = await userManager.CreateAsync(
                new MyIdentityUser
                { FirstName = memberRegisterVm.FirstName, LastName = memberRegisterVm.LastName, UserName = memberRegisterVm.Email, Email = memberRegisterVm.Email }, memberRegisterVm.Password);
            return result.Succeeded;
        }


    }
}
