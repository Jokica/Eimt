using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Dtos;
using Eimt.Application.Services;
using Emit.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Emit.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserManager userManager;

        public UserController(IUserService userService,IUserManager userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginViewModel loginViewModel)
        {
            var result = await userManager.Login(
                loginViewModel.Email, 
                loginViewModel.Password, 
                loginViewModel.RememberMe);
            if (result.Success)
                return RedirectToAction("Index", "Home");

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await userManager.LogOut();
            return NoContent();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(UserDto user)
        {
            userService.RegisterNewUser(user);
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ConfirmEmail(string email,string token)
        {
            var succes = userService.ConfirmUserToken(email, token);
            if(succes)
                return RedirectToAction("Login");
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}