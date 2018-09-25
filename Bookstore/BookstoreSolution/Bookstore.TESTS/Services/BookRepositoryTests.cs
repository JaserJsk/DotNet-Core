using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.TESTS.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.TESTS.Services
{
    public class BookRepositoryTests : IBookRepositoryTests
    {
        public void AddBookForAuthor(int authorId, Book book)
        {
            throw new NotImplementedException();
        }

        public bool AuthorExists(int authorId)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            String filename = "../Seeds/books.json";
            BookFromJSON myBooks = new API.Helpers.JSONHelper().JSON_File_To_Object<BookFromJSON>(filename);

            List<Book> allBooks = new List<Book>();

            foreach (var author in myBooks.books)
            {
                
                allBooks.Add(new Book { Title = author.Author, Price = author.Price, Stock = author.InStock }); 

            }

            return allBooks;
        }

        public IEnumerable<Book> GetAllBooksByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooksByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooksByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Author GetAuthor(int authorId, bool inludeBook)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public Book GetBookForAuthor(int authorId, int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBookForAuthors(int authorId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
