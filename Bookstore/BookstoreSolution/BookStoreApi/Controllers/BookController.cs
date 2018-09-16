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
