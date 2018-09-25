using Bookstore.API.Controllers;
using Bookstore.API.Interfaces;
using Bookstore.TESTS.Interfaces;
using Bookstore.TESTS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bookstore.TESTS.Controllers
{
    public class AuthorsControllerTests
    {
        AuthorsController _controller;
        IBookRepositoryTests _serviceTests;
        IBookRepository _service;

        //public AuthorsControllerTests(_service)
        //{
        //    _serviceTests = _service;
        //    _controller = new AuthorsController(_service);
        //}

        [Fact]
        public void GettingAllAuthors()
        {
            // Arrange
            var sut = _service.GetAllBooks();

            // Act
            Assert.NotEmpty(sut);
        }

    }
}
