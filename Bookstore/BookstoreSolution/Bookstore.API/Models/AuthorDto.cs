using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public int NumberOfBooks
        {
            get
            {
                return Books.Count;
            }
        }

        public ICollection<BookDto> Books { get; set; }
       = new List<BookDto>();

    }
}
