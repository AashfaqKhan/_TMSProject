using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DTO.DTOs
{
    public class CompaniesDTO
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage ="Please Enter Your Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Please Enter Your Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Please Enter Your Phone Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }
        public byte[]? Logo { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage ="Please Enter Your Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage ="Please Enter Your Confirm Password")]
        [Compare("Password",ErrorMessage ="Confirm Password Can't Matched!.")]
        public string? ConfirmPassword { get; set; }
    }
}
