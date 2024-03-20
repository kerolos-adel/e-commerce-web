using e_commerce_web.database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce_web.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public bool FeaturedProducts { get; set; }
        public string Image_ID { get; set; }
        public string UserId { get; set; }
        public bool IsFav { get; set; }
        public bool IsInCart { get; set; }
    }
}