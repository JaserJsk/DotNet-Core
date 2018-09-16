using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Models
{
    public class Item
    {
        public int Quantity { get; set; }

        public Book book { get; set; }
    }
}
