using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eimt.Application.Services;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.ResultModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emit.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService service;

        public IdentityController(IUserService service)
        {
            this.service = service;
        }
        [HttpPost]
        public Task<ForgotPasswordResult> ForgotPassword(ForgotPassword forgotPassword)
        {
           return service.ForgotPassword(forgotPassword.Email);
        }
    }
}