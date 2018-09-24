using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bookstore.API.Entities;
using Bookstore.APP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.APP.Models
{
    public class ApiGetBooks
    {
        public ApiGetBooks()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44315/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var apiroute = "api/authors/books";
            HttpResponseMessage response = client.GetAsync(apiroute).Result;

            var book = response.Content.ReadAsAsync<IEnumerable<ApiBook>>().Result;
            this.Books = book;
        }

        public IEnumerable<ApiBook> Books { get; set; }
    }
}
