using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Copy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Bookcase { get; set; }
        public int Shelf { get; set; }
        public int Status { get; set; }
    }
}
