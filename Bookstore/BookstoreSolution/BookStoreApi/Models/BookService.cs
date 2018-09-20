using BookStoreApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApi.Models
{
    public class BookService : IBook
    {
        private List<KeyValuePair<Book, int>> reservedCounts;
        private Book myBooks;
        public BookService()
        {
            reservedCounts = new List<KeyValuePair<Book, int>>();
            String filename = "./books.json";
            myBooks = new JSONHelper().JSON_File_To_Object<Book>(filename);
        }

        private Book FetchOriginalBooks()
        {
            String filename = "./books.json";
            return new JSONHelper().JSON_File_To_Object<Book>(filename);
        }

        public List<Book> GetAllBooks()
        {           
            /*if(myBooks==null)
            {
                String filename = "./books.json";
                myBooks = new JSONHelper().JSON_File_To_Object<Book>(filename);
            }*/
            return myBooks.books;
        }

        public Book GetByTitle(string title)
        {
            List<Book> books = GetAllBooks();
            if (books.Count == 0)
            {
                return GetEmptyBook();
            }
            foreach (var myBook in books)
            {
                if (myBook.Title == title)
                {
                    return myBook;
                }
            }
            return GetEmptyBook();
            //return GetAllBooks().Single(b => b.Title == title);
        }

        public Book GetByAuthor(string author)
        {
            List<Book> books = GetAllBooks();
            if (books.Count == 0)
            {
                return GetEmptyBook();
            }
            foreach (var myBook in books)
            {
                if (myBook.Author == author)
                {
                    return myBook;
                }
            }
            return GetEmptyBook();
            //return GetAllBooks().Single(b => b.Author == author);
        }        

        public BookPurchase ReserveCount(string author, string title, int count)
        {
            /*if (count <= 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            } */           
            List<Book> books = GetAllBooks();            
            if (books.Count == 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            }
            int index = 0;
            foreach (var myBook in books)
            {
                if (myBook.Title == title && myBook.Author == author)
                {
                    if(myBook.InStock == 0 && count > 0)
                    {
                        return new BookPurchase(GetEmptyBook(), 0);
                    }
                    else if (myBook.InStock < count)
                    {
                        myBooks.books.Remove(myBook);
                        int stock = myBook.InStock;
                        myBook.InStock = 0;                        
                        myBooks.books.Add(myBook);
                        return new BookPurchase(myBook, stock);                        
                    }
                    if (count > 0)
                    {
                        myBook.InStock -= count;
                    }
                    else
                    {
                        Book other = FetchOriginalBooks().books.ElementAt(index);
                        if(myBook.InStock - count <= other.InStock)
                        {
                            myBook.InStock -= count;
                            return new BookPurchase(myBook, other.InStock - myBook.InStock + count);
                        }
                        else
                        {
                            myBook.InStock = other.InStock;
                            return new BookPurchase(myBook, 0);
                        }
                    }
                    return new BookPurchase(myBook, count);
                }
                index++;
            }
            return new BookPurchase(GetEmptyBook(), 0);
        }

        public BookPurchase TryReserveCount(string author, string title, int count)
        {
            List<Book> books = GetAllBooks();            
            if (books.Count == 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            }
            foreach (var myBook in books)
            {
                if (myBook.Title == title && myBook.Author == author)
                {
                    if (myBook.InStock == 0 && count > 0)
                    {
                        return new BookPurchase(GetEmptyBook(), 0);
                    }
                    else if (myBook.InStock <= count)
                    {                                                
                        return new BookPurchase(myBook, myBook.InStock);                     
                    }   
                    return new BookPurchase(myBook, count);
                }
            }
            return new BookPurchase(GetEmptyBook(), 0);
        }

        public BookPurchase UnreserveCount(BookPurchase theBook, int count)
        {
            if (count <= 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            }
            List<Book> books = GetAllBooks();
            if (books.Count == 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            }
            foreach (var myBook in books)
            {
                if (myBook.Title == theBook.book.Title && myBook.Author == theBook.book.Author)
                {
                    if ((theBook.book.InStock == myBook.InStock)||(theBook.reserved==0))
                    {
                        return new BookPurchase(GetEmptyBook(), 0);
                    }
                    else if (theBook.reserved < count)
                    {
                        myBooks.books.Remove(myBook);
                        int stock = myBook.InStock;
                        myBook.InStock += theBook.reserved;
                        myBooks.books.Add(myBook);
                        return new BookPurchase(myBook, -theBook.reserved);
                    }
                    theBook.book.InStock += count;
                    return new BookPurchase(theBook.book, count - theBook.reserved);
                }
            }
            return new BookPurchase(GetEmptyBook(), 0);
        }

        public BookPurchase TryUnreserveCount(BookPurchase theBook, int count)
        {
            if (count <= 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            }
            List<Book> books = GetAllBooks();
            if (books.Count == 0)
            {
                return new BookPurchase(GetEmptyBook(), 0);
            }
            foreach (var myBook in books)
            {
                if (myBook.Title == theBook.book.Title && myBook.Author == theBook.book.Author)
                {
                    if ((theBook.book.InStock == myBook.InStock) || (theBook.reserved == 0))
                    {
                        return new BookPurchase(GetEmptyBook(), 0);
                    }
                    else if (theBook.reserved < count)
                    {                                                
                        return new BookPurchase(myBook, -theBook.reserved);
                    }                   
                    return new BookPurchase(theBook.book, count - theBook.reserved);
                }
            }
            return new BookPurchase(GetEmptyBook(), 0);
        }

        public Book GetByAuthorAndTitle(string author, string title)
        {
            List<Book> books = GetAllBooks();            
            if (books.Count == 0)
            {
                return GetEmptyBook();
            }
            foreach(var myBook in books)
            {
                if(myBook.Title == title && myBook.Author == author)
                {
                    return myBook;
                }
            }            
            return GetEmptyBook();
        }

        private Book GetEmptyBook()
        {
            Book emptyBook = new Book();
            emptyBook.Author = "n/a";
            emptyBook.Title = "n/a";
            emptyBook.InStock = 0;
            emptyBook.Price = 0;
            return emptyBook;
        }
    }
}
