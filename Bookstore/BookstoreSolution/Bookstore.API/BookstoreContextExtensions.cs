using System.IO;
using Bookstore.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Bookstore.API.Helpers;

namespace Bookstore.API
{
    public static class BookstoreContextExtensions
    {
        public static void EnsureSeedDataForContext(this BookstoreContext context)
        {
            if (!context.Authors.Any())
            {
                return;
            }

        }
    }
}
