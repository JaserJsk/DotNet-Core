using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApi.Interfaces;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _bookService;

        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _bookService.GetAllBooks();
        }

        // GET: api/Book/5
        [HttpGet("{title}", Name = "GetTitle")]
        public string GetTitle(string title)
        {
            return _bookService.GetByTitle(title).Title;
        }

        // GET: api/Book/5
        [HttpGet("{author}", Name = "GetAuthor")]
        public string GetAuthor(string author)
        {
            return _bookService.GetByTitle(author).Author;
        }

        //GET: api/Book/author/title
        [HttpGet("{author}/{title}", Name = "GetBookByAuthorAndTitle")]
        public Book GetBookByAuthorAndTitle(string author, string title)
        {
            return _bookService.GetByAuthorAndTitle(author, title);
        }

        //POST: api/Book/reserve/author/title/count
        [HttpGet("reserve/{author}/{title}/{count}", Name = "ReserveBookByAuthorAndTitle")]
        public BookPurchase GetReserveBookCount(string author, string title, int count)
        {            
            int c, i;
            var readme = "";
            BookPurchase tmpPurchase = null, resultNew = null;
            BookPurchase result = _bookService.TryReserveCount(author, title, count);
            var counter = (String)(HttpContext.Session.GetString("counter"));
            if(counter==null || counter.Length == 0)
            {
                HttpContext.Session.SetString("counter", "0");
                counter = "0";
            }
            else
            {
                c = Int32.Parse(counter) + 1;                
                counter = "" + c;
                HttpContext.Session.SetString("counter", counter);
            }
            c = Int32.Parse(counter) - 1;
            int olderCount = 0;
            if (c >= 0)
            {
                olderCount = 0;
                //for (i = 0; i <= c; i++)
                //{
                    readme = HttpContext.Session.GetString("reserve" + c);
                    tmpPurchase = new JSONHelper().JSON_String_To_Object<BookPurchase>(readme);
                    if(tmpPurchase.book.Author == result.book.Author && tmpPurchase.book.Title == result.book.Title)
                    {
                        //more reservations for same book
                        olderCount += tmpPurchase.reserved;
                    }
                //}
                resultNew = _bookService.TryReserveCount(author, title, count + olderCount);
            }
            
            if (resultNew != null)
            {
                if ((resultNew.reserved == result.reserved)||(resultNew.book.InStock==resultNew.reserved&&count>0))
                {
                    // no new reserved book
                    result = _bookService.ReserveCount(author, title, count + olderCount);
                    HttpContext.Session.SetString("reserve" + counter, new JSONHelper().Object_To_JSON_String<BookPurchase>(result));
                    HttpContext.Session.CommitAsync();

                    return result;
                }
                else
                {
                    result = _bookService.ReserveCount(author, title, count + olderCount);
                    HttpContext.Session.SetString("reserve" + counter, new JSONHelper().Object_To_JSON_String<BookPurchase>(result));
                    HttpContext.Session.CommitAsync();

                    return result;
                }
            }
            result = _bookService.ReserveCount(author, title, count);
            HttpContext.Session.SetString("reserve" + counter, new JSONHelper().Object_To_JSON_String<BookPurchase>(result));
            HttpContext.Session.CommitAsync();

            return result;
        }

        //POST: api/Book/reserve/author/title/count
        [HttpGet("unreserve/{author}/{title}/{count}", Name = "UnreserveBookByAuthorAndTitle")]
        public BookPurchase GetUnreserveBookCount(BookPurchase theBook, int count)
        {
            if(count<0)
            {
                return GetReserveBookCount(theBook.book.Author, theBook.book.Title, (-count));
            }
            int c, i;
            var readme = "";
            BookPurchase tmpPurchase = null, resultNew = null;
            BookPurchase result = _bookService.TryUnreserveCount(theBook, count);
            var counter = (String)(HttpContext.Session.GetString("urcounter"));
            if (counter == null || counter.Length == 0)
            {
                HttpContext.Session.SetString("urcounter", "0");
                counter = "0";
            }
            else
            {
                c = Int32.Parse(counter) + 1;
                counter = "" + c;
                HttpContext.Session.SetString("urcounter", counter);
            }
            c = Int32.Parse(counter) - 1;
            int olderCount = 0;
            if (c >= 0)
            {
                olderCount = 0;
                for (i = 0; i <= c; i++)
                {
                    readme = HttpContext.Session.GetString("unreserve" + c);
                    tmpPurchase = new JSONHelper().JSON_String_To_Object<BookPurchase>(readme);
                    if (tmpPurchase.book.Author == result.book.Author && tmpPurchase.book.Title == result.book.Title)
                    {
                        //more reservations for same book
                        olderCount += (-tmpPurchase.reserved);
                    }
                }
                resultNew = _bookService.TryUnreserveCount(new BookPurchase(theBook.book, olderCount), count);
            }

            HttpContext.Session.SetString("unreserve" + counter, new JSONHelper().Object_To_JSON_String<BookPurchase>(result));
            HttpContext.Session.CommitAsync();
            if (resultNew != null)
            {
                if ((resultNew.reserved == result.reserved) || (resultNew.book.InStock == resultNew.reserved && count > 0))
                {
                    // no new reserved book
                    // -1 will be the signal for app to mean that there is no more books in stock to reserve at the moment
                    return new BookPurchase(result.book, -1);
                }
                else
                {
                    return _bookService.UnreserveCount(theBook, count + olderCount);
                }
            }
            return _bookService.UnreserveCount(theBook, count);
        }

        // POST: api/Book
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
