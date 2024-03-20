using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(e_commerce_web.Startup1))]

namespace e_commerce_web
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            CookieAuthenticationOptions cookieAuthOptions = new CookieAuthenticationOptions();

            cookieAuthOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieAuthOptions.LoginPath = new PathString("/Account/Login");
            app.UseCookieAuthentication(cookieAuthOptions);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
