using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        [NavigationProperty]
        public RoleDTO Role { get; set; }
    }
}
