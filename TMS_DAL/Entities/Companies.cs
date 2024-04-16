using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.Common;

namespace TMS_DAL.Entities
{
    public class Companies:CommonField
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte[]? Logo { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }


    }
}
