using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CopyId { get; set; }
        [NavigationProperty]
        public CopyDTO Copy { get; set; }
        public int UserId { get; set; }
        [NavigationProperty]
        public UserDTO User { get; set; }
    }
}
