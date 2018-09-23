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
            if (context.Authors.Any())
            {
                return;
            }

            var authors = new List<Author>()
            {
                new Author()
                {
                    AuthorName = "Average Swede",
                    Book = new List<Book>()
                    {
                        new Book() {
                            Title = "Mastering åäö",
                            Price = 762,
                            Stock = 15
                        }
                    }
                },
                new Author()
                {
                    AuthorName = "Rich Block",
                    Book = new List<Book>()
                    {
                        new Book() {
                            Title = "How To Spend Money",
                            Price = 1000000,
                            Stock = 1
                        },
                        new Book() {
                            Title = "Desired",
                            Price = 564.5,
                            Stock = 0
                        }
                    }
                },
                new Author()
                {
                    AuthorName = "First Author",
                    Book = new List<Book>()
                    {
                        new Book() {
                            Title = "Generic Title",
                            Price = 185.5,
                            Stock = 5
                        }
                    }
                },
                new Author()
                {
                    AuthorName = "Second Author",
                    Book = new List<Book>()
                    {
                        new Book() {
                            Title = "Generic Title",
                            Price = 1748,
                            Stock = 3
                        }
                    }
                },
                new Author()
                {
                    AuthorName = "Cunning Bastard",
                    Book = new List<Book>()
                    {
                        new Book() {
                            Title = "Random Sales",
                            Price = 999,
                            Stock = 20
                        },
                        new Book() {
                            Title = "Random Sales",
                            Price = 499.5,
                            Stock = 3
                        },

                    }
                }
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

        }
    }
}
