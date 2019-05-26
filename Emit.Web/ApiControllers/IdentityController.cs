using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eimt.Application.Services;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.ResultModels;
using Eimt.Application.Services.ViewModels;
using Emit.Web.ClaimsPrincipalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emit.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IRoleService roleService;

        public IdentityController(IUserService service,IRoleService roleService)
        {
            this.service = service;
            this.roleService = roleService;
        }
        [HttpPost]
        public Task<ForgotPasswordResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            return service.ForgotPassword(forgotPassword.Email);
        }
   
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            IEnumerable<UserDto> users = new List<UserDto>();
            if (User.IsAdmin())
            {
                users = service.GetUsers();
            }
            else if (User.IsMenager())
            {
                users = service.GetUsersBySector(User.GetSector());
            }
            return Ok(new
            {
                draw = 0,
                recordsTotal = users.Count(),
                data = users.Where(x => x.Id != User.GetId<long>())
            });
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]long id)
        {
            service.DeleteUser(id);
            return NoContent();
        }
    }
}