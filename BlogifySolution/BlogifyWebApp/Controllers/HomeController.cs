using BlogifyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //2021-01-13 - Kadel D. Lacatt
        //HomeController contructor. Receives an instance of logger by DI.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //2021-01-13 - Kadel D. Lacatt
        //GET - Shows Home landing page
        public IActionResult Index()
        {
            return View();
        }

        //2021-01-13 - Kadel D. Lacatt
        //GET - Shows Privaty View with privacy policy content
        public IActionResult Privacy()
        {
            return View();
        }
        
        //2021-01-13 - Kadel D. Lacatt
        //GET - Catch errores pages
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
