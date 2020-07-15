using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.Accounts
{
    public class LoginModel
    {

        [Required(ErrorMessage="Email ID is required")]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required (ErrorMessage ="Password is required")]
        [Display(Name="Password")]

        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
