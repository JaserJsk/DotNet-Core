using BookStoreApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Models
{
    public class BookService : IBook
    {

        public List<Book> GetAllBooks()
        {
            String filename = "./books.json";
            Book myBooks = new JSONHelper().JSON_File_To_Object<Book>(filename);

            return myBooks.books;
        }

        public Book GetByTitle(string title)
        {
            return GetAllBooks().Single(b => b.Title == title);
        }

        public Book GetByAuthor(string author)
        {
            return GetAllBooks().Single(b => b.Author == author);
        }
    }
}
