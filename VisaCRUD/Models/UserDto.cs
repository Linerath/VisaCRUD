using System;
using System.Collections.Generic;
using System.Linq;
using VisaCRUD.DAL.Entities;

namespace VisaCRUD.Models.ViewModels
{
    public class UserDto
    {
        public String Login { get; set; }
        public List<Role> Roles { get; set; }

        public UserDto()
        {
            Roles = new List<Role>();
        }

        public bool HasRole(String role)
        {
            return Roles.Any(x => String.Equals(x.Name, role, StringComparison.OrdinalIgnoreCase));
        }
    }
}