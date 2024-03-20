using e_commerce_web.database;
using e_commerce_web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.Controllers
{
    public class AccountController : Controller
    {
        Account user = new Account();
        static Context context = new Context();
        static UserStore<Account> store = new UserStore<Account>(context);
        UserManager<Account> manager = new UserManager<Account>(store);

        // GET: Account
        public ActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }
            try
            {

                var result = await manager.FindAsync(userLogin.Email, userLogin.Password);


                if (result != null)
                {

                    await SignIn(result);

                        if (!string.IsNullOrEmpty(userLogin.ReturnUrl))
                        {
                            return Redirect(userLogin.ReturnUrl);
                        }
                    var userRole = manager.GetRoles(result.Id);
                    var role = userRole.FirstOrDefault();   
                    if(role =="Admin") 
                    {
                        return RedirectToAction("Index", "Category", new {area = "Admin"});
                    }
                    
                        return RedirectToAction("Index", "Home");
                   
                        
                   
                   
                }
                return View(userLogin);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userLogin);
            }
        }
        private async Task SignIn(Account account)
        {
            var identity = await manager.CreateIdentityAsync(account,DefaultAuthenticationTypes.ApplicationCookie);
            var owinContext = Request.GetOwinContext();
            var auth = owinContext.Authentication;
            auth.SignIn(identity);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationViewModel userRegister)
        {
            if(!ModelState.IsValid)
            {
                return View(userRegister);
            }
            try
            {
            

            
                user.UserName = userRegister.Username;
                user.PasswordHash = userRegister.Password;
                user.Email = userRegister.Email;
                user.Address = userRegister.Address;


               IdentityResult result = await manager.CreateAsync(user, user.PasswordHash);
                if(result.Succeeded)
                {
                    manager.AddToRole(user.Id, "Customer");
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    SignInManager<Account,string>signInManager = new SignInManager<Account, string>(manager, authenticationManager);
                    signInManager.SignIn(user, true, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", (result.Errors.ToList())[0]);
                    return View(userRegister);

                }

            }catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View(userRegister);
            }
           
        }

        public ActionResult SignOut()
        {
            IAuthenticationManager manager = HttpContext.GetOwinContext().Authentication;
            manager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}