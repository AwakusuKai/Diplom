using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Attributes;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        public int AutorId { get; set; }
        [NavigationProperty]
        [Display(Name = "Автор")]
        public Autor Autor { get; set; }
        public int GenreId { get; set; }
        [NavigationProperty]
        [Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        public int TypeId { get; set; }
        [NavigationProperty]
        [Display(Name = "Тип издания")]
        public Type Type { get; set; }
        [Display(Name = "Год")]
        public int PublishingYear { get; set; }
        public int PublishingHouseId { get; set; }
        [NavigationProperty]
        [Display(Name = "Издательство")]
        public PublishingHouse PublishingHouse { get; set; }
        [NavigationProperty]
        public bool BookStatus { get; set; }
        [NavigationProperty]
        [Display(Name = "Наличие")]
        public String StatusString {
            get
            {
                if (BookStatus == true)
                {
                    return "В наличии";
                }
                else 
                {
                    return "Нет в наличии";
                }
            }
        }
    }
}
