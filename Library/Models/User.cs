using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        [NavigationProperty]
        public Role Role { get; set; }

    }
}
