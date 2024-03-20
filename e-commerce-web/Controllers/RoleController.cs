using e_commerce_web.database;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace e_commerce_web.Controllers
{
    public class RoleController : Controller
    {
      
        // GET: Role
        public ActionResult NewRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> NewRole(string RoleName)
        {
            if(RoleName != null)
            {
                Context context = new Context();
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);
                IdentityRole role = new IdentityRole();
                role.Name = RoleName;
                IdentityResult result =await manager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    ViewBag.Error = result.Errors;
                    ViewBag.RoleName = RoleName;    
                    return View();
                }

            }
            return View();
        }
    }
}