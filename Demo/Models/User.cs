using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class User
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Uid")]
        public string U_id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string U_password { get; set; }
        //public string U_name { get; set; }
        //public string U_email { get; set; }
    }
}