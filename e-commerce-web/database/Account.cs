using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce_web.database
{
    public class Account : IdentityUser
    {
        public string Address {  get; set; }
        public virtual ICollection<Item> Orders { get; set; }
    }
}