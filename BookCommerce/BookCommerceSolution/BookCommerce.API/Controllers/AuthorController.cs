using AutoMapper;
using BookCommerce.API.Interfaces;
using BookCommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookCommerce.API.Controllers
{
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private IBookCommerceRepository _bookCommerceRepository;

        #region Constructor
        public AuthorController(IBookCommerceRepository bookStoreRepository)
        {
            _bookCommerceRepository = bookStoreRepository;
        }
        #endregion

        /* --------------------------------- */

        #region GET [ GetAuthors ]
        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorEntities = _bookCommerceRepository.GetAllAuthors();
            var results = Mapper.Map<IEnumerable<AuthorWithoutBookDto>>(authorEntities);

            return Ok(results);
        }
        #endregion

        #region GET [ GetAuthor ]
        [HttpGet("{authorid}")]
        public IActionResult GetAuthor(int authorid, bool includeBook = false)
        {
            var author = _bookCommerceRepository.GetAuthor(authorid, includeBook);

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
