using e_commerce_web.database;
using e_commerce_web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.Controllers
{
   
    public class HomeController : Controller
    {
        
        Context context = new Context();
        //[Authorize]
        public ActionResult Index()
        {
            
            ViewBag.cat = context.Categories.ToList();
            var user = User.Identity.GetUserName();
            var userr = User.Identity.GetUserId();

            ViewBag.name = user;

            var item = context.Items.Select(x => new ItemViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                UserId = x.UserId,
                Quantity = x.Quantity,
                Image_ID = x.Image_ID,
                Discount = x.Discount,
                FeaturedProducts= x.FeaturedProducts,
                IsFav = context.FavItems.Any(d => d.ItemId == x.Id && d.UserId == userr),
                IsInCart = context.Carts.Any(z=>z.ItemId==x.Id && z.UserId == userr),
                
            }).ToList();
           
            
            return View(item);
        }
      
        public ActionResult Search(string ItemName) 
        {
             var userr = User.Identity.GetUserId();
            var user = User.Identity.GetUserName();

            ViewBag.cat = context.Categories.ToList();
            ViewBag.name = user;

            var item = context.Items.Where(c => c.Name.Contains(ItemName)).Select(x => new ItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    UserId = x.UserId,
                    Quantity = x.Quantity,
                    Image_ID = x.Image_ID,
                    Discount = x.Discount,
                    FeaturedProducts = x.FeaturedProducts,
                    IsFav = context.FavItems.Any(d => d.ItemId == x.Id && d.UserId == userr),
                    IsInCart = context.Carts.Any(z => z.ItemId == x.Id && z.UserId == userr),

                }).ToList();

            return View(item);
            
        }

      
        public ActionResult role()
        {

            return View();
        }
        [HttpPost]
        public async Task <ActionResult> role(string roleName)
        {
            Context context = new Context();
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
            
           IdentityRole role = new IdentityRole();
            role.Name = roleName;
           IdentityResult result = await roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                return View();
            }
            return View();
        }

        public ActionResult MyOrders()
        {
            var user = User.Identity.GetUserId();

            var orders = context.Orders.Where(d=>d.UserId==user).ToList();

            return View(orders);
            
        }
        public ActionResult MyOrdersDetails(int id)
        {

            var itemOreder = context.ItemsOrderds.Where(o => o.OrderId == id).ToList();
            var username = User.Identity.GetUserName();
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
        public ActionResult ItemInformation(int Id)
        {
            var item = context.Items.FirstOrDefault(o => o.Id == Id);
            var item1 = context.Items.FirstOrDefault(o => o.Id == Id).CategoryId;
            var cat = context.Categories.FirstOrDefault(c => c.Id == item1);
            ViewBag.catName = cat.Name;

            return View(item);
        }

        public ActionResult DeleteOrder(int Id)
        {
            var order = context.Orders.FirstOrDefault(d => d.Id == Id);
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("MyOrders");
        }



    }
}