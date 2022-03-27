using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AutorId { get; set; }
        [NavigationProperty]
        public AutorDTO Autor { get; set; }
        public int GenreId { get; set; }
        [NavigationProperty]
        public GenreDTO Genre { get; set; }
        public int TypeId { get; set; }
        [NavigationProperty]
        public TypeDTO Type { get; set; }
        public int PublishingYear { get; set; }
        public int PublishingHouseId { get; set; }
        [NavigationProperty]
        public PublishingHouseDTO PublishingHouse { get; set; }
    }
}
