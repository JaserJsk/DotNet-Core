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

namespace Bookstore.APP.Controllers
{
    public class BookController : Controller
    {
        #region Index
        // GET: Book
        public ActionResult Index(string search_param, string x)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44315/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var apiRoute = "api/authors/books";

            if (x != null && x.Trim() != "")
            {
                if (search_param == "title")
                {
                    apiRoute = "api/authors/books/search/title/" + x;
                }
                else if (search_param == "author")
                {
                    apiRoute = "api/authors/books/search/author/" + x;
                }
                else if (search_param == "all")
                {
                    apiRoute = "api/authors/books/search/all/" + x;
                }
            }

            HttpResponseMessage response = client.GetAsync(apiRoute).Result;

            var book = response.Content.ReadAsAsync<IEnumerable<ApiBook>>().Result;
            BookPageModel bpm = new BookPageModel();
            bpm.ApiBooks = book;
            bpm.searchTerm = x;
            bpm.searchType = search_param;

            return View(bpm);
        } 
        #endregion

        #region Details
        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        } 
        #endregion

        #region Create
        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        } 
        #endregion

        #region Create
        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 
        #endregion

        #region Edit
        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        } 
        #endregion

        #region Edit
        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 
        #endregion

        #region Delete
        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        } 
        #endregion

        #region Delete
        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 
        #endregion
    }
}