using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.APP.Models
{
    public class ApiBook
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public string Author { get; set; }

        public int AuthorId { get; set; }
    }
}
