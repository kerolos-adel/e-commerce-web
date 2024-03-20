using e_commerce_web.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace e_commerce_web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            ViewBag.cat = context.Categories.ToList();
            var item = context.Items.ToList();
            return View(item);
        }
        public ActionResult Create()
        {
            
            ViewBag.cat = context.Categories.ToList();
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Item item)
        {
            ViewBag.cat = context.Categories.ToList();

            try
            {
                if (ModelState.IsValid)
                {
                    item.Image_ID = SaveImageFile(item.ImageFile);

                    Item cat = context.Items.FirstOrDefault(s => s.Name == item.Name);
                    if (cat != null)
                    {

                        ModelState.AddModelError("", "Name Already Exist");
                        return View(item);
                    }

                    context.Items.Add(item);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                    return View(item);
                }
            }
            catch (Exception ex){
               ViewBag.ex = ex.Message;
                return View(item);
            }
        }
        public ActionResult Delete(int id)
        {
            var item = context.Items.FirstOrDefault(c => c.Id == id);
            context.Items.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.cat = context.Categories.ToList();
            var item = context.Items.FirstOrDefault(i => i.Id == id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(Item item)
        {
            ViewBag.cat = context.Categories.ToList();
            if (ModelState.IsValid)
            {
                var newItem = context.Items.FirstOrDefault(x=>x.Id==item.Id);
                newItem.Image_ID = SaveImageFile(item.ImageFile, item.Image_ID);
                newItem.Name = item.Name;
                newItem.CategoryId = item.CategoryId;
                newItem.Price = item.Price;
                newItem.Quantity = item.Quantity;
                newItem.Discount = item.Discount;
                newItem.FeaturedProducts = item.FeaturedProducts;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        private string SaveImageFile(HttpPostedFileBase imageFile, string currentimageId = "")
        {
          
            if (imageFile != null)
            {
              
                var fileExtention = Path.GetExtension(imageFile.FileName);
                var imageGuid = Guid.NewGuid().ToString();
                string ImageId = imageGuid + fileExtention;

                string filePath = Server.MapPath($"~/uploads/images/{ImageId}");
                imageFile.SaveAs(filePath);

                if (!string.IsNullOrEmpty(currentimageId))
                {
                    string oldFilePath = Server.MapPath($"~/uploads/images/{currentimageId}");
                    System.IO.File.Delete(oldFilePath);

                }
                return ImageId;
            }
            return currentimageId;
        }
    }
}