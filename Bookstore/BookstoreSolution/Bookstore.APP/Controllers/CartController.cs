using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.APP.Helpers;
using Bookstore.APP.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.APP.Controllers
{
    public class CartController : Controller
    {
        #region Index
        [Route("Cart")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.apiBook.Price * item.count);
            }
            else
            {
                cart = new List<CartPageModel>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                ViewBag.cart = cart;
            }
            return View();
        }
        #endregion

        #region Notify
        [HttpPost]
        [Route("Cart/Notify/{id}/{add}")]
        public void Notify(int id, int add)
        {
            var notes = SessionHelper.GetObjectFromJson<List<IdCountModel>>(HttpContext.Session, "notes");
            if (notes != null)
            {
                bool found = false;
                int index = 0;
                foreach (var n in notes)
                {
                    if (n.Id == id)
                    {
                        notes.ElementAt(index).Count = add;
                        found = true;
                        break;
                    }
                    index++;
                }
                if (!found)
                {
                    notes.Add(new IdCountModel() { Id = id, Count = add });
                }
                ViewBag.notes = notes;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "notes", notes);
            }
            else
            {
                notes = new List<IdCountModel>();
                notes.Add(new IdCountModel() { Id = id, Count = add });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "notes", notes);
                ViewBag.notes = notes;
            }
        }
        #endregion

        #region Query
        [HttpPost]
        [Route("Cart/Query/{id}/{added}")]
        public bool Query(int id, int added)
        {
            ApiBook thebook = new ApiGetBooks().Books.Where(b => b.Id == id).FirstOrDefault();
            int index = isExist(id);
            if (index == -1)
            {
                if (thebook != null && thebook.Stock >= added)
                {
                    return true;
                }
            }
            else
            {
                List<CartPageModel> cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
                if (thebook != null && thebook.Stock >= added + cart[index].count)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Buy
        [Route("Cart/Buy/{id}/{added}")]
        public IActionResult Buy(int id, int added)
        {
            var notes = SessionHelper.GetObjectFromJson<List<IdCountModel>>(HttpContext.Session, "notes");
            if (notes != null)
            {
                foreach (var cmodel in notes)
                {
                    if (cmodel.Id == id)
                    {
                        added = cmodel.Count;
                        break;
                    }
                }
            }

            CartPageModel productModel = new CartPageModel();
            if (SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart") == null)
            {
                List<CartPageModel> cart = new List<CartPageModel>();
                ApiBook thebook = new ApiGetBooks().Books.Where(b => b.Id == id).FirstOrDefault();
                if (thebook != null && thebook.Stock >= added)
                {
                    cart.Add(new CartPageModel { apiBook = thebook, count = added });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartPageModel> cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                ApiBook thebook;
                if (index != -1)
                {
                    thebook = new ApiGetBooks().Books.Where(b => b.Id == id).FirstOrDefault();
                    if (thebook != null && thebook.Stock >= added + cart[index].count)
                    {
                        cart[index].count += added;
                    }
                }
                else
                {
                    thebook = new ApiGetBooks().Books.Where(b => b.Id == id).FirstOrDefault();
                    if (thebook != null && thebook.Stock >= added)
                    {
                        cart.Add(new CartPageModel { apiBook = thebook, count = added });
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Remove
        [Route("Cart/Remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<CartPageModel> cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
            int index = isExist(id);
            if (index != -1)
            {
                cart.RemoveAt(index);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        #endregion

        #region isExist
        private int isExist(int id)
        {
            List<CartPageModel> cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                return -1;
            }
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].apiBook.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        } 
        #endregion
    }
}