using e_commerce_web.database;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.Controllers
{
    public class FavController : Controller
    {
        Context context = new Context();
        public ActionResult AddToFav(int itemId)
        {
            
            var user = User.Identity.GetUserId();
            if (user == null)
            {
                return Json(new { value = false, islogin = false });
            }
            else
            {
                FavItem favItem = new FavItem();
                if (context.FavItems.Any(d => d.ItemId == itemId && d.UserId == user))
                {
                    context.FavItems.RemoveRange(context.FavItems.Where(d => d.ItemId == itemId && d.UserId == user));
                    context.SaveChanges();
                    return Json(new { value = false });

                }
                else
                {
                    favItem.UserId = user;
                    favItem.ItemId = itemId;
                    context.FavItems.Add(favItem);
                    context.SaveChanges();
                    return Json(new { value = true });

                }
            }

        }
        public ActionResult ShowFavItems()
        {

            var user = User.Identity.GetUserId();
            var favItem = context.FavItems.Include("Items").Where(d => d.UserId == user).ToList();
            ViewBag.username = User.Identity.GetUserName();



            return View(favItem);
        }
        public ActionResult DeleteItem(int Id)
        {
            var item = context.FavItems.FirstOrDefault(d => d.ItemId == Id);
            context.FavItems.Remove(item);
            context.SaveChanges();
            return RedirectToAction("ShowFavItems");
        }
    }
}