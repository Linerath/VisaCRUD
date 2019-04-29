using System.Web.Mvc;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;

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
            Visa model = null;
            if (countryId != null)
                model = visasRepository.GetVisaByCountryId(countryId.Value);

            return View(model);
        }
    }
}