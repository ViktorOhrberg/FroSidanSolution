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
    }
}
