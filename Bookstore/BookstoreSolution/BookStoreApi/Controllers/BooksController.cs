using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public ActionResult<IEnumerable<Book>> Get()
        {

            String filename = "Files/books.json";
            Book myBooks = new JSONHelper().JSON_File_To_Object<Book>(filename);

            //String myBooksStr = new JSONHelper().JSON_URL_To_JSON_String("https://archive.senseidev.com/temp/json/data.json");
            //Book myBooksFromURL = new JSONHelper().JSON_String_To_Object<Book>(myBooksStr);

            int counter = 1;
            foreach (var book in myBooks.books)
            {
                Console.WriteLine("Book {0}", counter);
                Console.WriteLine(book.Title);
                Console.WriteLine(book.Author);
                Console.WriteLine(book.Price);
                Console.WriteLine(book.InStock);
                Console.WriteLine();
                counter++;
            }

            return myBooks.books;

        }

    }

}