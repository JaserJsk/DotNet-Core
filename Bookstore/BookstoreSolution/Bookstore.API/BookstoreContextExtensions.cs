using System.IO;
using Bookstore.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Bookstore.API.Helpers;
using Bookstore.API.Models;

namespace Bookstore.API
{
    public static class BookstoreContextExtensions
    {      
        private static void AddAuthorFromBook(ref List<Author> authors, BookFromJSON book)
        {
            /*
             * Add new author from Book - Book object contains author data
             * Here book object is the version read from JSON, with original structure, but
             * it is saved in the format used by the API - classes Author and Book. */
            authors.Add(new Author()
            {
                // Fetch author name
                AuthorName = book.Author,
                Book = new List<Book>()
                    {
                        new Book()
                        {
                            Title = book.Title,
                            Price = book.Price,
                            Stock = book.InStock
                        }
                    }
            });
        }

        public static void EnsureSeedDataForContext(this BookstoreContext context)
        {
            if (context.Authors.Any())
            {
                return;
            }

            // Prepare list of authors
            var authors = new List<Author>();

            // Read JSON data with helper into myBooks object
            String filename = "./Seeds/books.json";
            BookFromJSON myBooks = new Helpers.JSONHelper().JSON_File_To_Object<BookFromJSON>(filename);

            // Parse through myBooks.books to get the real book data
            foreach (BookFromJSON book in myBooks.books)
            {
                // Current book has no author yet = add it
                if (authors.Count == 0)
                {
                    // Use function defined above, authors passed as "ref" because its value changes and
                    // We need the changes after the function was already executed (AddAuthorFromBook)
                    AddAuthorFromBook(ref authors, book);
                }
                else
                {
                    bool found = false;
                    int i = 0;

                    // Try to locate the author of the current book
                    while (i < authors.Count && found == false)
                    {
                        if(authors.ElementAt(i).AuthorName == book.Author)
                        {
                            found = true;
                            continue;
                        }
                        i++;
                    }

                    // If the author is found
                    if (found)
                    {
                        // Add this book to the same author, which has other book(s)
                        authors.ElementAt(i).Book.Add(new Book()
                        {
                            Title = book.Title,
                            Price = book.Price,
                            Stock = book.InStock
                        });
                    }
                    else
                    {
                        // If author is not found, we need to add it
                        AddAuthorFromBook(ref authors, book);
                    }
                }
            }

            // Update context to save all changes with authors
            context.Authors.AddRange(authors);
            context.SaveChanges();

        }
    }
}
