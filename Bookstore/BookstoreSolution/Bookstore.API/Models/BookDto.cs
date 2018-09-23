using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }
    }
}
