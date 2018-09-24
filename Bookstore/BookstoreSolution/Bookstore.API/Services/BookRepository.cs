using Bookstore.API.Entities;
using Bookstore.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Services
{
    public class BookRepository : IBookRepository
    {
        private BookstoreContext _context;

        #region Constructor
        public BookRepository(BookstoreContext context)
        {
            _context = context;
        } 
        #endregion

        #region AuthorExists
        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }
        #endregion

        #region GetAllBooks
        public IEnumerable<Book> GetAllBooks()
        {
            var authors = this.GetAuthors();
            List<Book> allBooks = new List<Book>();
            foreach(var author in authors)
            {
                var authBooks = this.GetBookForAuthors(author.Id);
                foreach(var oneBook in authBooks)
                {
                    oneBook.Author = author;
                    allBooks.Add(oneBook);
                }
            }
            return allBooks;
        }
        #endregion

        #region GetAllBooksByTitle
        public IEnumerable<Book> GetAllBooksByTitle(string title)
        {
            var authors = this.GetAuthors();
            List<Book> allBooks = new List<Book>();
            foreach (var author in authors)
            {
                var authBooks = this.GetBookForAuthors(author.Id);
                foreach (var oneBook in authBooks)
                {
                    oneBook.Author = author;
                    if (oneBook.Title.ToLower().Contains(title.ToLower()) ||
                        oneBook.Title.Contains(title))
                    {
                        allBooks.Add(oneBook);
                    }
                }
            }
            return allBooks;
        }

        #region GetAllBooksByAuthor
        public IEnumerable<Book> GetAllBooksByAuthor(string authorname)
        {
            var authors = this.GetAuthors();
            List<Book> allBooks = new List<Book>();
            foreach (var author in authors)
            {
                var authBooks = this.GetBookForAuthors(author.Id);
                foreach (var oneBook in authBooks)
                {
                    oneBook.Author = author;
                    if (oneBook.Author.AuthorName.ToLower().Contains(authorname.ToLower()) ||
                        oneBook.Author.AuthorName.Contains(authorname))
                    {
                        allBooks.Add(oneBook);
                    }
                }
            }
            return allBooks;
        }
        #endregion

        #region GetAllBooksByTerm
        public IEnumerable<Book> GetAllBooksByTerm(string term)
        {
            var authors = this.GetAuthors();
            List<Book> allBooks = new List<Book>();
            foreach (var author in authors)
            {
                var authBooks = this.GetBookForAuthors(author.Id);
                foreach (var oneBook in authBooks)
                {
                    oneBook.Author = author;
                    if (oneBook.Author.AuthorName.ToLower().Contains(term.ToLower()) || 
                        oneBook.Title.ToLower().Contains(term.ToLower()) ||
                        oneBook.Author.AuthorName.Contains(term) ||
                        oneBook.Title.Contains(term))
                    {
                        allBooks.Add(oneBook);
                    }
                }
            }
            return allBooks;
        }
        #endregion
        #endregion

        #region GetAuthors
        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.OrderBy(a => a.AuthorName).ToList();
        }
        #endregion

        #region GetAuthor
        public Author GetAuthor(int authorId, bool inludeBook)
        {
            if (inludeBook)
            {
                return _context.Authors.Include(a => a.Book)
                    .Where(a => a.Id == authorId).FirstOrDefault();
            }

            return _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        }
        #endregion

        #region GetBookForAuthors
        public IEnumerable<Book> GetBookForAuthors(int authorId)
        {
            return _context.Books
                .Where(b => b.AuthorId == authorId).ToList();
        }
        #endregion

        #region GetBookForAuthor
        public Book GetBookForAuthor(int authorId, int bookId)
        {
            return _context.Books
                .Where(b => b.AuthorId == authorId && b.Id == bookId).FirstOrDefault();
        }
        #endregion

        #region AddBookForAuthor
        public void AddBookForAuthor(int authorId, Book book)
        {
            var author = GetAuthor(authorId, false);
            author.Book.Add(book);
        }
        #endregion

        #region DeleteBook
        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }
        #endregion

        #region Save
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        #endregion
    }
}
