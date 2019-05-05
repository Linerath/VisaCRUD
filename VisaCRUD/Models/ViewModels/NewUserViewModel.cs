using System;
using System.ComponentModel.DataAnnotations;

namespace VisaCRUD.Models.ViewModels
{
    public class NewUserViewModel
    {
        [Required(ErrorMessage = "Логин не введен")]
        [MinLength(3, ErrorMessage = "Логин должен содержать минимум 3 символа")]
        public String Login { get; set; }
        [Required(ErrorMessage = "Пароль не введен")]
        [MinLength(1, ErrorMessage = "Пароль должен содержать минимум 1 символов")]
        public String Password { get; set; }
    }
}