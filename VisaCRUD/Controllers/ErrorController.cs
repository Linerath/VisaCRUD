using System.Web.Mvc;

namespace VisaCRUD.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Forbidden()
        {
            return View();
        }
    }
}