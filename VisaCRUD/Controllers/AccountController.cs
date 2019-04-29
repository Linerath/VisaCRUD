using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisaCRUD.Models;

namespace VisaCRUD.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Login(LoginViewModel model)
        {
            return RedirectToRoute(new { controller = "Visa", action = "All" });
        }
    }
}