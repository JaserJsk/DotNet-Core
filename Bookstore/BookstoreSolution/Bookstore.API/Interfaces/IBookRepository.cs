using Bookstore.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Interfaces
{
    public interface IBookRepository
    {
        bool AuthorExists(int authorId);

        IEnumerable<Author> GetAuthors();

        Author GetAuthor(int authorId, bool inludeBook);

        IEnumerable<Book> GetBookForAuthors(int authorId);

        Book GetBookForAuthor(int authorId, int bookId);

        void AddBookForAuthor(int authorId, Book book);

        void DeleteBook(Book book);

        bool Save();
    }
}
