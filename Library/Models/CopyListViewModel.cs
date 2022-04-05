using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class CopyListViewModel
    {
        public IEnumerable<Copy> Copies { get; set; }
        public SelectList Books { get; set; }
        public Copy Item;



    }
}
