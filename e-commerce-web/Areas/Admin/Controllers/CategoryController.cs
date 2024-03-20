using e_commerce_web.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        Context context = new Context();
        //[AllowAnonymous]
        //[Authorize]
        //[Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var cat = context.Categories.ToList();
            return View(cat);
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                Category cat = context.Categories.FirstOrDefault(s=>s.Name== category.Name);
                if (cat != null)
                {
                    ModelState.AddModelError("", "Name Already Exist");
                    return View(category);
                }

                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                return View(category); 
            }
        }
        public ActionResult Delete(int id)
        {
            var cat = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Categories.Remove(cat);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HandleError]
        public ActionResult Edit(int id)
        {
            var cat = context.Categories.FirstOrDefault(i=>i.Id==id);
            return View(cat);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
           var newCat = context.Categories.FirstOrDefault();
            newCat.Name = category.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var cat = context.Categories.FirstOrDefault(d=>d.Id==id);
            return View(cat.Items);
        }
    }
}