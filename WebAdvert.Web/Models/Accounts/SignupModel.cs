
using System.ComponentModel.DataAnnotations;


namespace WebAdvert.Web.Models.Accounts
{
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        [Display (Name ="Email")]
        public string Email { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [StringLength (6, ErrorMessage ="Password must contain atleast 6 characters")]
        [Display (Name ="Password")]
        public string Password { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [Display (Name ="ConfirmPassword")]
        [Compare ("Password",ErrorMessage ="Password and Confirm Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
