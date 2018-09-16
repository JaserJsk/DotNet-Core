using BookStoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Interfaces
{
    public interface IBook
    {
        List<Book> GetAllBooks();
        Book GetByTitle(string title);
        Book GetByAuthor(string author);
    }
}
