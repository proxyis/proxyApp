using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            List<ProxyModel> proxies = new List<ProxyModel>();
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "proxy", "proxy.txt");
            using (var stream = new StreamReader(path))
            {
                await stream.ReadLineAsync();

                string line;
                while((line = await stream.ReadLineAsync()) != null)
                {
                    proxies.Add(new ProxyModel() { Volume = line });
                }
            }
            return View(proxies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
