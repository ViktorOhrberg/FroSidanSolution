using FroSidanMVC.Models;
using FroSidanMVC.Models.ViewModels.Members;
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
        [HttpGet]
        [Route("MyPages")]
        public IActionResult MyPages()
        {
            return View(new MyPagesVM { Username = User.Identity.Name });
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new MembersLoginVM { ReturnUrl = returnUrl });
            
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(MembersLoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Check if credentials is valid (and set auth cookie)
            var success = await mService.TryLoginAsync(viewModel);
            if (!success)
            {
                // Show error
                ModelState.AddModelError(nameof(MembersLoginVM.Username), "Login failed");
                return View(viewModel);
            }

            // Redirect user
            if (string.IsNullOrWhiteSpace(viewModel.ReturnUrl))
                return RedirectToAction(nameof(MyPages));
            else
                return Redirect(viewModel.ReturnUrl);
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(MemberRegisterVM vM)
        {
            if (!ModelState.IsValid)
                return View(vM);

            // Try to register user
            var success = await mService.TryRegisterAsync(vM);
            if (!success)
            {
                // if not succesful, Show error
                ModelState.AddModelError(string.Empty, "Misslyckades att skapa konto");
                return View(vM);
            }

            // if succesful, redirect user
            return RedirectToAction(nameof(Login));
        }
    }
}
