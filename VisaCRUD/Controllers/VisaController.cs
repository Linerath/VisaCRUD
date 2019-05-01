using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Visa> visas = (countryId != null)
                ? visasRepository.GetAll(countryId.Value)
                : null;

            CountryInfoViewModel model = new CountryInfoViewModel
            {
                SelectedCountryId = countryId,
                Visas = visas,
            };

            return View(model);
        }

        [HttpGet]
        public ViewResult Add()
        {
            AddVisaViewModel model = new AddVisaViewModel
            {
                Countries = visasRepository.GetAllCountries(),
                ServiceTypes = visasRepository.GetAllServiceTypes(),
                Documents = visasRepository.GetAllDocuments(),
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToRouteResult Add(NewVisaViewModel visa)
        {
            Visa newVisa = new Visa
            {
                Country = new Country
                {
                    Id = visa.Country,
                },
                Terms = visa.Terms,
                Validity = visa.Validity,
                Period = visa.Period,
                Number = visa.Number,
                WebSite = visa.WebSite,
            };

            if (visa.ServiceType.HasValue)
                newVisa.ServiceType = new ServiceType { Id = visa.ServiceType.Value };

            if (visa.Documents?.Length > 0)
                newVisa.Documents = visa.Documents.Select(x => new Document{ Id = x }).ToList();

            visasRepository.Add(newVisa);

            return RedirectToAction("Info", new { countryId = visa.Country });
        }

        [HttpGet]
        public ActionResult Edit(int visaId)
        {
            EditVisaViewModel model = new EditVisaViewModel
            {
                Visa = visasRepository.GetVisaById(visaId),
                Countries = visasRepository.GetAllCountries(),
                ServiceTypes = visasRepository.GetAllServiceTypes(),
                Documents = visasRepository.GetAllDocuments(),
            };

            if (model.Visa == null)
                return RedirectToAction("Info");

            return View(model);
        }

        [HttpPost]
        public RedirectToRouteResult Edit(NewVisaViewModel visa)
        {
            if (!visa.Id.HasValue)
                throw new ArgumentException("id");

            Visa newVisa = new Visa
            {
                Id = visa.Id.Value,
                Country = new Country
                {
                    Id = visa.Country,
                },
                Terms = visa.Terms,
                Validity = visa.Validity,
                Period = visa.Period,
                Number = visa.Number,
                WebSite = visa.WebSite,
            };

            if (visa.ServiceType.HasValue)
                newVisa.ServiceType = new ServiceType { Id = visa.ServiceType.Value };

            if (visa.Documents?.Length > 0)
                newVisa.Documents = visa.Documents.Select(x => new Document { Id = x }).ToList();

            visasRepository.Update(newVisa);

            return RedirectToAction("Info", new { countryId = visa.Country });
        }

        public RedirectToRouteResult Delete(int visaId, int countryId)
        {
            visasRepository.Delete(visaId);

            return RedirectToAction("Info", new { countryId });
        }
    }
}