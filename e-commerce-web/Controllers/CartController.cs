using e_commerce_web.database;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.Controllers
{
    public class CartController : Controller
    {
        double total = 0;
        Context context = new Context();
        public ActionResult AddToCart(int itemId)
        {

            var user = User.Identity.GetUserId();
            if (user==null)
            {
                return Json(new { value = false, islogin = false });
            }
            else { 
            Cart order = new Cart();
            if (context.Carts.Any(d => d.ItemId == itemId && d.UserId == user))
            {
                context.Carts.RemoveRange(context.Carts.Where(d => d.ItemId == itemId && d.UserId == user));
                context.SaveChanges();
                return Json(new { value = true });

            }
            else
            {
                order.UserId = user;
                order.ItemId = itemId;
                    var price = context.Items.FirstOrDefault(s => s.Id == itemId);
                    if (price.Discount > 0)
                    {
                        order.Price = price.Price - price.Discount;
                    }
                    else
                    {
                        order.Price = context.Items.FirstOrDefault(s => s.Id == itemId).Price;

                    }
                    order.Quantity = context.Items.FirstOrDefault(s => s.Id == itemId).Quantity;
                context.Carts.Add(order);
                context.SaveChanges();
                return Json(new { value = false });
            }
            }
        }
      
        public ActionResult ShowCartItems()
        {

            var user = User.Identity.GetUserId();
            var items = context.Carts.Include("Items").Where(d => d.UserId == user).ToList();
            double totalprice = 0;
            foreach (var item in items)
            {
                totalprice += item.Price * item.Quantity;
            }
            ViewBag.totalprice = totalprice;
            total = totalprice;


            return View(items);
        }
        public ActionResult DeleteItem(int Id)
        {
            var item = context.Carts.FirstOrDefault(d => d.ItemId == Id);
                context.Carts.Remove(item);
                context.SaveChanges();
                return RedirectToAction("ShowCartItems");
        }
        [HttpPost]
        
        public ActionResult Increase(int itemId)
        {
            var order = context.Carts.Include("Items").FirstOrDefault(d => d.Id == itemId);
            order.Quantity++;
            context.SaveChanges();
            var items = context.Carts.Where(d => d.UserId == order.UserId).ToList();
            double totalprice = 0;
            foreach(var item in items)
            {
                totalprice += item.Price * item.Quantity;
            }
            return Json(new { Quantity = order.Quantity, Price = order.Quantity * order.Price ,Totalprice=totalprice});
        }
        [HttpPost]
        public ActionResult Decrease(int itemId)
        {
            var order = context.Carts.Include("Items").FirstOrDefault(d => d.Id == itemId);
            order.Quantity--;
            context.SaveChanges();
            var items = context.Carts.Where(d => d.UserId == order.UserId).ToList();
            double totalprice = 0;
            foreach (var item in items)
            {
                totalprice += item.Price * item.Quantity;
            }
            return Json(new { Quantity = order.Quantity, Price = order.Quantity * order.Price, Totalprice = totalprice });
        }
        public ActionResult Order()
        {
            var user = User.Identity.GetUserId();
            Order order = new Order();
            order.UserId = user;
            double total = 0;
            var totalprice = context.Carts.Where(d => d.UserId == user).ToList();
            foreach(var item in totalprice)
            {

                total += item.Price*item.Quantity;
            }
            order.TotlaPrice = total;
            context.Orders.Add(order);
            context.SaveChanges();


            var username = User.Identity.GetUserName();
            var items = context.Carts.Include("Items").Where(d => d.UserId == user).ToList();
           
            foreach (var item in items)
            {
                ItemsOrderd itemsOrderd = new ItemsOrderd {
                    
                    OrderId = order.Id,
                    ItemsId = item.ItemId,
                    ItemQuantity = item.Quantity,
                };
                context.ItemsOrderds.Add(itemsOrderd);
            }
            context.SaveChanges();
            var cart = context.Carts.Where(d => d.UserId == user);
            foreach (var item in cart)
            {
                context.Carts.Remove(item);
            }
            
            
            context.SaveChanges();
            return RedirectToAction("Index","Home");

        }
  


    }
}