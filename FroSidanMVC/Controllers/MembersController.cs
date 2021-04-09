using FroSidanMVC.Models;
using FroSidanMVC.Models.ViewModels.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FroSidanMVC.Controllers
{
    public class MembersController : Controller
    {
        MembersService mService;
        private readonly ProductsService pService;

        public MembersController(MembersService mService, ProductsService pService)
        {
            this.mService = mService;
            this.pService = pService;
        }

        [Authorize]
        [HttpGet]
        [Route("MyPages")]
        public IActionResult MyPagesAsync()
        {
            var orders = mService.GetOrdersAsync();

            return View(new MyPagesVM { Username = User.Identity.Name, Orders = orders });
        }

        [Authorize]
        [HttpGet]
        [Route("MyOrder/{orderID}")]
        public async Task<IActionResult> MyOrderAsync(int orderID)
        {
            var shoppingCartJSON = pService.GetShoppingCartFromOrder(orderID);
            List<int> shoppingCart = JsonConvert.DeserializeObject<List<int>>(shoppingCartJSON);
            var orders = await pService.GetSummaryVMAsync(shoppingCart);

            MyOrderLineVM[] model = orders.Select(x => new MyOrderLineVM
            {
                Id = x.Id,
                Name = x.Name,
                OrderId = orderID,
                Price = x.Price,
                Quantity = x.Quantity,
                Status = "Under behandling",
            })
                .ToArray();
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl == null)
                returnUrl = "MyPages";

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
                return RedirectToAction(nameof(MyPagesAsync));
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

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await mService.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
