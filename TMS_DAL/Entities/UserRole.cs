using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.Common;

namespace TMS_DAL.Entities
{
    public class UserRole:CommonField
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
