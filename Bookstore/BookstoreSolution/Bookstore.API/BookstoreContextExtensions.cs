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
            authors.Add(new Author()
            {
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

            var authors = new List<Author>();

            String filename = "./Seeds/books.json";
            BookFromJSON myBooks = new Helpers.JSONHelper().JSON_File_To_Object<BookFromJSON>(filename);

            foreach(BookFromJSON book in myBooks.books)
            {
                if(authors.Count == 0)
                {
                    AddAuthorFromBook(ref authors, book);
                }
                else
                {
                    bool found = false;
                    int i = 0;
                    while(i < authors.Count && found == false)
                    {
                        if(authors.ElementAt(i).AuthorName == book.Author)
                        {
                            found = true;
                            continue;
                        }
                        i++;
                    }
                    if(found)
                    {
                        authors.ElementAt(i).Book.Add(new Book()
                        {
                            Title = book.Title,
                            Price = book.Price,
                            Stock = book.InStock
                        });
                    }
                    else
                    {
                        AddAuthorFromBook(ref authors, book);
                    }
                }
            }

            context.Authors.AddRange(authors);
            context.SaveChanges();

        }
    }
}
