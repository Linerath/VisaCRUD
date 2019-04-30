using System.Collections.Generic;
using VisaCRUD.DAL.Entities;

namespace VisaCRUD.Models.ViewModels
{
    public class CountriesViewModel : BaseSidebarViewModel
    {
        public List<Country> Countries { get; set; }
    }
}