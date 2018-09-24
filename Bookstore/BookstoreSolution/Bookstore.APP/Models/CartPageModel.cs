using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.APP.Models
{
    public class CartPageModel
    {
        public int Count { get; set; }

        public ApiBook apiBook { get; set; }
    }
}
