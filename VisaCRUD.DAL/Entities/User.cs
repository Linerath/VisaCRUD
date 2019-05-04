using System;
using System.Collections.Generic;

namespace VisaCRUD.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}
