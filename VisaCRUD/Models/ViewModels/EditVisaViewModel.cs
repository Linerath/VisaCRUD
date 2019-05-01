using VisaCRUD.DAL.Entities;

namespace VisaCRUD.Models.ViewModels
{
    public class EditVisaViewModel : AddVisaViewModel
    {
        public Visa Visa { get; set; }
    }
}