using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FashionDream.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Exceed 20 char!!")]
        public string Name { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Min 6 char")]
        [MaxLength(15, ErrorMessage = "Not more then 15 char!!")]
        public string Password { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public  string Email { get; set; }
    }
}