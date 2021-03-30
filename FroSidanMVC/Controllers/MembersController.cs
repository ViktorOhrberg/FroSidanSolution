using FroSidanMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Controllers
{
    public class MembersController : Controller
    {
        MembersService mService; 
        public MembersController(MembersService mService)
        {
            this.mService = mService;
        }

        [Authorize]
        public IActionResult Members()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("mypages")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("products/mypages")]
        [HttpGet]
        public IActionResult MyPages()
        {
            return View();
        }
        [Route("members/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(MemberRegisterVM vM)
        {
            if (!ModelState.IsValid)
                return View(vM);

            // Try to register user
            var success = await mService.TryRegisterAsync(vM);
            if (!success)
            {
                // Show error
                ModelState.AddModelError(string.Empty, "Failed to create user");
                return View(vM);
            }

            // Redirect user
            return RedirectToAction(nameof(Login));
        }
    }
}
