using System.Web.Mvc;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.Infrastructure;
using VisaCRUD.Models.ViewModels;

namespace VisaCRUD.Controllers
{
    [RoleResult]
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

        public PartialViewResult Sidebar(int? selectedCountryId)
        {
            CountriesViewModel model = new CountriesViewModel
            {
                SelectedCountryId = selectedCountryId,
                Countries = visasRepository.GetAllCountries(),
            };

            return PartialView("SidebarPartial", model);
        }
    }
}