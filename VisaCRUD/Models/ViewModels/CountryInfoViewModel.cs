using System.Collections.Generic;
using VisaCRUD.DAL.Entities;

namespace VisaCRUD.Models.ViewModels
{
    public class CountryInfoViewModel : BaseSidebarViewModel
    {
        public List<Visa> Visas { get; set; }
    }
}