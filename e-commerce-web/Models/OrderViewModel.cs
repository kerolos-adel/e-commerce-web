using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce_web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double TotlaPrice { get; set; }
        public string DelivaryName { get; set; }
        public string Days { get; set; }

        public bool isAccepted { get; set; }
    }
}