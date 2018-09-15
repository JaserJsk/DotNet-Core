using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Interfaces
{
    public interface IBook
    {
        string Title { get; }

        string Author { get; }

        decimal Price { get; }
    }
}
