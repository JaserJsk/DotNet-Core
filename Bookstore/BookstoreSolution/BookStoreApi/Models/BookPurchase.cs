using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Models
{
    public class BookPurchase
    {
        public BookPurchase(Book b, int r)
        {
            book = b;
            reserved = r;
        }
        public Book book { get; set; }
        public int reserved { get; set; }
    }
}
