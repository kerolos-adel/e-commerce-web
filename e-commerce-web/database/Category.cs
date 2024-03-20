using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.database
{
    public class Category
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}