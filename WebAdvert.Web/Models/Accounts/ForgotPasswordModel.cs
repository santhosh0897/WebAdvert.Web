using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.Accounts
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage ="Enter Email Id")]
        [EmailAddress]
        [Display(Name ="EmailId")]
        public string Email { get; set; }

        
    }
}
