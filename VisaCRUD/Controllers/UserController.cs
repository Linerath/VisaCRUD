using System;
using System.Web.Mvc;
using System.Web.Security;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.Infrastructure.Interfaces;
using VisaCRUD.Models.ViewModels;
using VisaCRUD.Security;

namespace VisaCRUD.Controllers
{
    public class UserController : Controller
    {
        private IAuthProvider authProvider;
        private IUsersRepository usersRepository;

        public UserController(IAuthProvider authProvider, IUsersRepository usersRepository)
        {
            this.authProvider = authProvider;
            this.usersRepository = usersRepository;
        }

        [HttpGet]
        public ViewResult Login(String message = null)
        {
            return View(message);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUri)
        {
            if (ModelState.IsValid)
            {
                UserDto user = authProvider.Authenticate(model?.Login, model?.Password);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.Login, DateTime.Now, DateTime.Now.AddMinutes(20), true, "bi4");

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
        public ActionResult CreateAccount(NewUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!usersRepository.IsLoginUnique(model.Login))
            {
                ModelState.AddModelError("", "Такой логин уже существует. Пожалуйста, введите другой");
                return View();
            }

            String passwordHash = CryptoService.GetHashCode(model.Password);
            if (!usersRepository.AddUser(new User
            {
                Login = model.Login,
                Password = passwordHash,
            }))
            {
                ModelState.AddModelError("", "Произошла ошибка.");
                return View();
            }

            ViewBag.Message = "Аккаунт успешно создан";

            return View();
        }
    }
}