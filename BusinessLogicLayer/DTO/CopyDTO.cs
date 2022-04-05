using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Attributes;

namespace BusinessLogicLayer.DTO
{
    public class CopyDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [NavigationProperty]
        public BookDTO Book { get; set; }
        public int Bookcase { get; set; }
        public int Shelf { get; set; }
        public int Status { get; set; }
    }
}
