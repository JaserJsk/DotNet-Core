using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class BookFromJSON
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public int InStock { get; set; }

        public List<BookFromJSON> books { get; set; }        
    }
}
