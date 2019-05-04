using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VisaCRUD.Infrastructure.Interfaces;
using VisaCRUD.Models.ViewModels;

namespace VisaCRUD.Controllers
{
    public class UserController : Controller
    {
        private IAuthProvider authProvider;

        public UserController(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUri)
        {
            if (ModelState.IsValid)
            {
                UserDto user = authProvider.Authenticate(model?.Login, model?.Password);

                if (user != null)
                {
                    if (!String.IsNullOrWhiteSpace(returnUri))
                        return Redirect(returnUri);
                    return RedirectToRoute(new { controller = "Visa", action = "Info" });
                }
                else
                {
                    ModelState.AddModelError("", "Введен неверный логин и/или пароль");
                    return View();
                }
            }

            return View();
        }

        public RedirectToRouteResult Logout()
        {
            authProvider.Logout();
            
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ViewResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ViewResult CreateAccount(NewUserViewModel model)
        {
            return View();
        }
    }
}