using System.Collections.Generic;
using VisaCRUD.DAL.Entities;

namespace VisaCRUD.Models.ViewModels
{
    public class AddVisaViewModel : BaseSidebarViewModel
    {
        public List<Country> Countries { get; set; }
        public List<ServiceType> ServiceTypes { get; set; }
        public List<Document> Documents { get; set; }
    }
}