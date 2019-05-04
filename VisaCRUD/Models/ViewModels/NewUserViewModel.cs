using System;

namespace VisaCRUD.Models.ViewModels
{
    public class NewUserViewModel
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public String PasswordConfirm { get; set; }
    }
}