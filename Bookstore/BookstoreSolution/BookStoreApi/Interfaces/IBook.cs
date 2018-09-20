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
        Book GetByAuthorAndTitle(string author, string title);
        BookPurchase ReserveCount(string author, string title, int count);
        BookPurchase TryReserveCount(string author, string title, int count);
        BookPurchase UnreserveCount(BookPurchase theBook, int count);
        BookPurchase TryUnreserveCount(BookPurchase theBook, int count);
    }
}
