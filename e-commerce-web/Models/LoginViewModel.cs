using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce_web.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("username")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}