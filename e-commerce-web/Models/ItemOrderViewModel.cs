using e_commerce_web.database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_commerce_web.Models
{
    public class ItemOrderViewModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}