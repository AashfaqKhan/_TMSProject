using System;
using TMS_DAL.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TMS_DAL.Entities
{
    public class User : CommonField
    {
        [Key]
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string PasswordSlat { get; set; }
        public Guid RoleId { get; set; }
        public string? UserName { get; set; }
        public int GenderId { get; set; }
        public DateTime DOB { get; set; }
        public string? Address1 { get; set; }
        public bool Status { get; set; }
        public string? PhoneNo { get; set; }
        public Guid? CompanyId { get; set; }
        //public string? RoleName { get; set; }
    }
}
