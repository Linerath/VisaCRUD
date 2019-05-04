﻿using System;
using System.ComponentModel.DataAnnotations;

namespace VisaCRUD.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public String Login { get; set; }
        [Required]
        public String Password { get; set; }
    }
}