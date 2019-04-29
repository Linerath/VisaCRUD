using System.Collections.Generic;
using System.Web.Mvc;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;

namespace VisaCRUD.Controllers
{
    public class NavigationController : Controller
    {
        private IVisasRepository visasRepository;

        public NavigationController(IVisasRepository visasRepository)
        {
            this.visasRepository = visasRepository;
        }

        public PartialViewResult Navbar()
        {
            return PartialView("NavbarPartial");
        }

        public PartialViewResult Sidebar()
        {
            List<Country> model = visasRepository.GetAllCountries();

            return PartialView("SidebarPartial", model);
        }
    }
}