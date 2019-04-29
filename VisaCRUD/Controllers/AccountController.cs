using System.Web.Mvc;
using VisaCRUD.Models.ViewModels;

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