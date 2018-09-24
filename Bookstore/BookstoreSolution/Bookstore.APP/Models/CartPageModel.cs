using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.APP.Models
{
    public class CartPageModel
    {
        public ApiBook apiBook { get; set; }

        public int count { get; set; }
    }
}
