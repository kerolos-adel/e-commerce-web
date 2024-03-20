using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce_web.database
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        public double Price { get; set; }
        public double Discount { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool FeaturedProducts { get; set; }
       
       
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Account User { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public string Image_ID { get; set; }
    }
}