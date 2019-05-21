using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emit.Web.Models;
using Eimt.Domain.DomainModels;
using Eimt.Persistence;
using Microsoft.AspNetCore.SignalR;
using Emit.Web.Hubs;

namespace Emit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<NotificationHub> hubContext;

        public HomeController(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
             await hubContext.Clients.All.SendAsync("Login","Test");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
