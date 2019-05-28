using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Dtos;
using Eimt.Application.Services;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.ViewModels;
using Emit.Web.ClaimsPrincipalExtensions;
using Emit.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Emit.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserManager userManager;
        private readonly IRoleService roleService;
        private readonly ISectorService sectorService;
        private readonly IDocumentService documentService;

        public UserController(IUserService userService,
            IUserManager userManager,
            IRoleService roleService,
            ISectorService sectorService,
            IDocumentService documentService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.roleService = roleService;
            this.sectorService = sectorService;
            this.documentService = documentService;
        }
        [Route("signin/{provider}")]
        public IActionResult SignIn(string provider, string returnUrl = null) =>
        Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);

        [HttpGet]
        public IActionResult Login()=> View();
        [HttpGet]
        public IActionResult Register() => View();
        [HttpGet]
        public IActionResult ForgotPassword() => View();
        [Authorize]
        public IActionResult Profile() => View();
        [Authorize]
        public IActionResult Index() => View(GetUsersBasedOnRole());
        [Authorize]
        public IActionResult ChangePassword() => View();


        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginViewModel loginViewModel)
        {
            var result = await userManager.Login(
                loginViewModel.Email,
                loginViewModel.Password,
                loginViewModel.RememberMe);
            if (result.Success)
                return RedirectToAction("Index");

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await userManager.LogOut();
            return RedirectToAction("Login");
        }
  
        public IActionResult Register(RegisterUserDto user)
        {
            userService.RegisterNewUser(user);
            return View();
        }
  
        [HttpGet]
        public IActionResult ConfirmEmail(string email, string token)
        {
            var succes = userService.ConfirmUserToken(email, token);
            if (succes)
                return RedirectToAction("Login");
            return View();
        }

 
        [HttpGet("user/{id}/edit")]
        public IActionResult Edit(long id)
        {
            var userRoles = roleService.GetUserRoles(id).ToList();
            var model = new EditViewModel
            {
                UserRoles = userRoles,
                Roles = roleService.GetRoles().Where(x => !userRoles.Contains(x)).ToList(),
                Id = id,
                Sectors = sectorService.GetSectorNames().ToList()
            };
            return View(model);
        }
        [HttpGet("user/details")]
        public IActionResult Details()
        {
            return View(userService.GetUser(User.GetId<long>()));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm]ChangePassword changePassword)
        {
            var changePasswrod = new ChangePasswordDto
            {
                Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                OldPassword = changePassword.OldPassword,
                Password = changePassword.NewPassword
            };
            userService.ChangePassword(changePasswrod);
            await userManager.LogOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult CSV()
        {
            var users = GetUsersBasedOnRole();
            var exel = documentService.ToDocument(users.ToList());
            return new FileContentResult(exel, "text/csv")
            {
                FileDownloadName = "User.xlsx"
            };
        }
        private IEnumerable<UserDto> GetUsersBasedOnRole()
        {
            IEnumerable<UserDto> users = new List<UserDto>();
            if (User.IsAdmin())
            {
                users = userService.GetUsers();
            }
            else if (User.IsMenager())
            {
                users = userService.GetUsersBySector(User.GetSector());
            }
            return users = users.Where(x => x.Id != User.GetId<long>());
        }
    }
}
