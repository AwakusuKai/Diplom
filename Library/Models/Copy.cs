using BusinessLogicLayer.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Copy
    {
        [Display(Name = "ID экземпляра")]
        public int Id { get; set; }
        public int BookId { get; set; }
        [NavigationProperty]
        public Book Book { get; set; }
        [Display(Name = "Шкаф")]
        public int Bookcase { get; set; }
        [Display(Name = "Полка")]
        public int Shelf { get; set; }
        public int Status { get; set; }
        [NavigationProperty]
        [Display(Name = "Статус")]
        public string StatusString {
            get 
            {
                if (Status == 0)
                {
                    return "В наличии";
                }
                if (Status == 1)
                {
                    return "Забронирован";
                }
                else
                {
                    return "На руках";
                }
            }
        }
    }
}
