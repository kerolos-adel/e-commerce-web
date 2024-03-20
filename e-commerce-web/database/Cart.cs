using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_commerce_web.database
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Items { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        
    }
}