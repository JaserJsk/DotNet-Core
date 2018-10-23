using BookCommerce.APP.Helpers;
using BookCommerce.APP.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCommerce.APP.Controllers
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
                ViewBag.total = cart.Sum(item => item.bookModel.Price * item.Count);
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
            BookModel thebook = new ApiGetBooks().ApiFetchedBooks.Where(b => b.Id == id).FirstOrDefault();
            int index = DoesExist(id);
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
                if (thebook != null && thebook.Stock >= added + cart[index].Count)
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
                BookModel thebook = new ApiGetBooks().ApiFetchedBooks.Where(b => b.Id == id).FirstOrDefault();
                if (thebook != null && thebook.Stock >= added)
                {
                    cart.Add(new CartPageModel { bookModel = thebook, Count = added });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartPageModel> cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
                int index = DoesExist(id);
                BookModel thebook;
                if (index != -1)
                {
                    thebook = new ApiGetBooks().ApiFetchedBooks.Where(b => b.Id == id).FirstOrDefault();
                    if (thebook != null && thebook.Stock >= added + cart[index].Count)
                    {
                        cart[index].Count += added;
                    }
                }
                else
                {
                    thebook = new ApiGetBooks().ApiFetchedBooks.Where(b => b.Id == id).FirstOrDefault();
                    if (thebook != null && thebook.Stock >= added)
                    {
                        cart.Add(new CartPageModel { bookModel = thebook, Count = added });
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
            int index = DoesExist(id);
            if (index != -1)
            {
                cart.RemoveAt(index);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        #endregion

        #region Does exist
        private int DoesExist(int id)
        {
            List<CartPageModel> cart = SessionHelper.GetObjectFromJson<List<CartPageModel>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                return -1;
            }
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].bookModel.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

    }
}
