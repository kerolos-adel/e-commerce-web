using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace e_commerce_web.database
{
    public class Context : IdentityDbContext<Account>
    {
        public Context():base("Data Source=.;Initial Catalog=e-commerce-web;Integrated Security=True;Encrypt=False") 
        {
            
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<ItemsOrderd> ItemsOrderds { get; set; }
        public virtual DbSet<FavItem> FavItems { get; set; }

        


    }
}