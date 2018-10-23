using System;

namespace BookCommerce.APP.Models
{
    public class CartPageModel
    {
        public int Count { get; set; }

        public BookModel bookModel { get; set; }
    }
}
