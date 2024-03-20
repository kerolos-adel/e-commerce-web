using e_commerce_web.database;
using e_commerce_web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        Context context = new Context();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = context.Orders.ToList();

            return View(orders);
        }
        public ActionResult Details(int id)
        {
          
            var itemOreder = context.ItemsOrderds.Where(o => o.OrderId == id).ToList();
            var itemOreder1 = context.ItemsOrderds.FirstOrDefault(o => o.OrderId == id);

            var ord = context.Orders.FirstOrDefault(o => o.Id == itemOreder1.OrderId).UserId;

            var username = context.Users.FirstOrDefault(c => c.Id == ord).UserName;
            ViewBag.username = username;
            ViewBag.IdOrder = id;
            List<ItemOrderViewModel> list = new List<ItemOrderViewModel>();
            foreach (var item in itemOreder)
            {
                ItemOrderViewModel itemOrderViewModel = new ItemOrderViewModel
                {
                    Id = item.Id,
                    ItemName = context.Items.Find(item.ItemsId).Name,
                    UserName = username,
                    Price = context.Items.Find(item.ItemsId).Price,
                    Quantity = item.ItemQuantity,
                };
                list.Add(itemOrderViewModel);
            }

            return View(list);
        }


        public ActionResult AcceptOrder(int id) 
        {
            var itemOreder = context.Orders.FirstOrDefault(o => o.Id == id);
            if (context.Orders.Any(d => d.Id == id && d.isAccepted == true)) 
            {
                itemOreder.isAccepted = false;
                context.SaveChanges();
                return Json(new { value = true });
            }
            else
            {
                itemOreder.isAccepted = true;
                context.SaveChanges();
                return Json(new { value = false });

            }


        }

        public ActionResult Accept(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Accept(Order order,int id)
        {
            var itemOreder = context.Orders.FirstOrDefault(o => o.Id == id);
            itemOreder.isAccepted=true;
            itemOreder.DelivaryName = order.DelivaryName;
            itemOreder.Days = order.Days;
            context.SaveChanges();
            return RedirectToAction("Index");
            //if (context.Orders.Any(d => d.Id == id && d.isAccepted == true))
            //{
            //    itemOreder.isAccepted = false;
                
            //    return Json(new { value = true });
            //}
            //else
            //{
            //    itemOreder.isAccepted = true;
            //    context.SaveChanges();
            //    return Json(new { value = false });

            //}
           
        }


    }
}