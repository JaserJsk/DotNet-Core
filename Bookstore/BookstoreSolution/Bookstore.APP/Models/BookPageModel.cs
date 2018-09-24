using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.APP.Models
{
    public class BookPageModel
    {
        public string searchTerm { get; set; }

        public string searchType { get; set; }

        public IEnumerable<ApiBook> ApiBooks { get; set; }
    }
}
