using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_commerce_web.database
{
    public class ItemsOrderd
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemsId { get; set; }
        public int ItemQuantity { get; set; }





    }
}