using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class AuthorWithoutBookDto
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }
    }
}
