using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_DTO.DTOs
{
    public class UserDTO
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? PasswordSlat { get; set; }
        public int GenderId { get; set; }
        public Guid RoleId { get; set; }
        public string? UserName { get; set; }
        public DateTime DOB { get; set; }
        public string? Address1 { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "Phone No is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNo { get; set; }

        public string RoleName { get; set; }

        public Guid? CompanyId{ get; set; }
    }
}
