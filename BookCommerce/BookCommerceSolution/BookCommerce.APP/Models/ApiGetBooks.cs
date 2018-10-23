using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BookCommerce.APP.Models
{
    public class ApiGetBooks
    {
        public ApiGetBooks()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var apiRoute = "api/authors/books";
            HttpResponseMessage response = client.GetAsync(apiRoute).Result;

            var fetchedBook = response.Content.ReadAsAsync<IEnumerable<BookModel>>().Result;
            this.ApiFetchedBooks = fetchedBook;
        }

        public IEnumerable<BookModel> ApiFetchedBooks { get; set; }
    }
}
