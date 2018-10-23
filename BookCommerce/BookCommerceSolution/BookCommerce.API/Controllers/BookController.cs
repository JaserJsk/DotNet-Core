using AutoMapper;
using BookCommerce.API.Interfaces;
using BookCommerce.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BookCommerce.API.Controllers
{
    [Route("api/authors")]
    public class BookController : Controller
    {
        private ILogger<BookController> _logger;
        private IMailService _mailService;
        private IBookCommerceRepository _bookCommerceRepository;

        #region Constructor
        public BookController(ILogger<BookController> logger,
            IMailService mailService,
            IBookCommerceRepository bookStoreRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _bookCommerceRepository = bookStoreRepository;
        }
        #endregion

        /* --------------------------------- */

        #region GET [ Get All Books ]
        [HttpGet("books")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var allBooks = _bookCommerceRepository.GetAllBooks();

                var allBooksResults = Mapper.Map<IEnumerable<BookWithAuthorDto>>(allBooks);

                return Ok(allBooksResults);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exeption while getting all books from all authors", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        #endregion

        #region GET [ Get Books By Id ]
        [HttpGet("{authorid}/books")]
        public IActionResult GetBooksById(int authorid)
        {
            try
            {
                if (!_bookCommerceRepository.AuthorExists(authorid))
                {
                    _logger.LogInformation($"Author with id {authorid} was not found when accessing books.");
                    return NotFound();
                }

                var bookForAuthor = _bookCommerceRepository.GetBookForAuthors(authorid);

                var bookForAuthorResults = Mapper.Map<IEnumerable<BookDto>>(bookForAuthor);

                return Ok(bookForAuthorResults);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exeption while getting book for author with id {authorid}.", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        #endregion

        #region GET [ Get All Books By Author ]
        [HttpGet("books/search/author/{author}")]
        public IActionResult GetAllBooksByAuthor(string author)
        {
            try
            {
                var allBooks = _bookCommerceRepository.GetAllBooksByAuthor(author);

                var allBooksResults = Mapper.Map<IEnumerable<BookWithAuthorDto>>(allBooks);

                return Ok(allBooksResults);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exeption while getting all books from all authors", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        #endregion

        #region GET [ Get All Books By Title ]
        [HttpGet("books/search/title/{title}")]
        public IActionResult GetAllBooksByTitle(string title)
        {
            try
            {
                var allBooks = _bookCommerceRepository.GetAllBooksByTitle(title);

                var allBooksResults = Mapper.Map<IEnumerable<BookWithAuthorDto>>(allBooks);

                return Ok(allBooksResults);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exeption while getting all books from all authors", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        #endregion

        #region GET [ Get All Books By Term ]
        [HttpGet("books/search/all/{term}")]
        public IActionResult GetAllBooksByTerm(string term)
        {
            try
            {
                var allBooks = _bookCommerceRepository.GetAllBooksByTerm(term);

                var allBooksResults = Mapper.Map<IEnumerable<BookWithAuthorDto>>(allBooks);

                return Ok(allBooksResults);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exeption while getting all books from all authors", ex);
                return StatusCode(500, "A problem happend while handeling your request.");
            }
        }
        #endregion

        #region GET [ GetBook Name = "GetBook" ]
        [HttpGet("{authorid}/books/{id}", Name = "GetBook")]
        public IActionResult GetBook(int authorid, int id)
        {
            if (!_bookCommerceRepository.AuthorExists(authorid))
            {
                return NotFound();
            }

            var book = _bookCommerceRepository.GetBookForAuthor(authorid, id);
            if (book == null)
            {
                return NotFound();
            }

            var booktResult = Mapper.Map<BookDto>(book);

            return Ok(booktResult);
        }
        #endregion

        /* --------------------------------- */

        #region POST [ CreateBook ]
        [HttpPost("{authorid}/books")]
        public IActionResult CreateBook(int authorid,
            [FromBody] BookForCreationDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (book.Description == book.Title)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finalBook = Mapper.Map<Entities.Book>(book);

            _bookCommerceRepository.AddBookForAuthor(authorid, finalBook);

            if (!_bookCommerceRepository.Save())
            {
                return StatusCode(500, "A problem happend while handeling your request.");
            }

            var createdBookToReturn = Mapper.Map<Models.BookDto>(finalBook);

            return CreatedAtRoute("GetBook", new
            { authorId = authorid, id = createdBookToReturn.Id }, createdBookToReturn);
        }
        #endregion

        /* --------------------------------- */

        #region PUT [ UpdateBook ]
        [HttpPut("{authorid}/books/{id}")]
        public IActionResult UpdateBook(int authorid, int id,
            [FromBody] BookForUpdateDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (book.Description == book.Title)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookEntity = _bookCommerceRepository.GetBookForAuthor(authorid, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(book, bookEntity);

            if (!_bookCommerceRepository.Save())
            {
                return StatusCode(500, "A problem happend while handeling your request.");
            }

            // Means that the request completed successfully but there is nothing to return
            return NoContent();
        }
        #endregion

        #region PATCH [ PartiallyUpdateBook ]
        [HttpPatch("{authorid}/books/{id}")]
        public IActionResult PartiallyUpdateBook(int authorid, int id,
            [FromBody] JsonPatchDocument<BookForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_bookCommerceRepository.AuthorExists(authorid))
            {
                return NotFound();
            }

            var bookEntity = _bookCommerceRepository.GetBookForAuthor(authorid, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            var bookToPatch = Mapper.Map<BookForUpdateDto>(bookEntity);

            patchDoc.ApplyTo(bookToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bookToPatch.Description == bookToPatch.Title)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the title.");
            }

            TryValidateModel(bookToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(bookToPatch, bookEntity);

            if (!_bookCommerceRepository.Save())
            {
                return StatusCode(500, "A problem happend while handeling your request.");
            }

            // Means that the request completed successfully but there is nothing to return
            return NoContent();
        }
        #endregion

        /* --------------------------------- */

        #region DELETE [ DeleteBook ]
        [HttpDelete("{authorid}/books/{id}")]
        public IActionResult DeleteBook(int authorid, int id)
        {
            if (!_bookCommerceRepository.AuthorExists(authorid))
            {
                return NotFound();
            }

            var bookEntity = _bookCommerceRepository.GetBookForAuthor(authorid, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            _bookCommerceRepository.DeleteBook(bookEntity);

            if (!_bookCommerceRepository.Save())
            {
                return StatusCode(500, "A problem happend while handeling your request.");
            }

            _mailService.Send("Book deleted.",
                    $"Book {bookEntity.Title} with id {bookEntity.Id} was deleted.");

            // Means that the request completed successfully but there is nothing to return
            return NoContent();
        }
        #endregion
    }
}
