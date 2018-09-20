using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Models
{
    public class Item
    {
        public Book book { get; set; }

        public int quantity { get; set; }
    }
}
