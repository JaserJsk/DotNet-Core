using System;

namespace BookCommerce.API.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime PublishingDate { get; set; }
        
    }
}
