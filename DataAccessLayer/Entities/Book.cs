using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int PublishingYear { get; set; }
        public int PublishingHouseId { get; set; }
        public PublishingHouse PublishingHouse { get; set; }

    }
}
