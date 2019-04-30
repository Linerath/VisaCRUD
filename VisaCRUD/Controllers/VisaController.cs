using System.Web.Mvc;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.Models.ViewModels;

namespace VisaCRUD.Controllers
{
    public class VisaController : Controller
    {
        private IVisasRepository visasRepository;

        public VisaController(IVisasRepository visasRepository)
        {
            this.visasRepository = visasRepository;
        }

        public ViewResult Info(int? countryId = null)
        {
            Visa visa = (countryId != null)
                ? visasRepository.GetVisaByCountryId(countryId.Value)
                : null;

            VisaViewModel model = new VisaViewModel
            {
                SelectedCountryId = countryId,
                Visa = visa,
            };

            return View(model);
        }
    }
}