using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookStoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44392/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/book").Result;

            var book = response.Content.ReadAsAsync<IEnumerable<Book>>().Result;
            return View(book);
        }

        public ActionResult Reserve(string author, string title, int count)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44392/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/book/" + author + title + count).Result;

            var reserved = response.Content.ReadAsAsync<Book>().Result;
            return View(reserved);
        }

    }

}