using System.Web.Mvc;

namespace VisaCRUD.Controllers
{
    public class NavigationController : Controller
    {
        public PartialViewResult Navbar()
        {
            return PartialView("NavbarPartial");
        }

        public PartialViewResult Sidebar()
        {
            return PartialView("SidebarPartial");
        }
    }
}