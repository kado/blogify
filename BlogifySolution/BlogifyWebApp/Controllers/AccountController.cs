using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogifyWebApp.Controllers
{
    public class AccountController : Controller
    {
        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult Login()
        //Handles GET request to /Account/Login, shows the Login view where users
        //con start session.
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
