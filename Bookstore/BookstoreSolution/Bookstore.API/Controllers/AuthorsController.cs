using AutoMapper;
using Bookstore.API.Interfaces;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private IBookRepository _bookRepository;

        #region Constructor
        public AuthorsController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        } 
        #endregion

        #region GET [ GetAuthors ]
        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorEntities = _bookRepository.GetAuthors();
            var results = Mapper.Map<IEnumerable<AuthorWithoutBookDto>>(authorEntities);

            return Ok(results);
        }
        #endregion

        #region GET [ GetAuthor ]
        [HttpGet("{authorid}")]
        public IActionResult GetAuthor(int authorid, bool includeBook = false)
        {
            var author = _bookRepository.GetAuthor(authorid, includeBook);

            if (author == null)
            {
                return NotFound();
            }

            if (includeBook)
            {
                var authorResult = Mapper.Map<IEnumerable<AuthorDto>>(author);

                return Ok(authorResult);
            }

            var authorWithoutBookResult = Mapper.Map<AuthorWithoutBookDto>(author);

            return Ok(authorWithoutBookResult);
        }
        #endregion
    }
}
