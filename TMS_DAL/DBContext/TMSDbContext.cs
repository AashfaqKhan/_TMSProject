using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DTO.DTOs;
using TMS_DAL.Entities;

namespace TMS_DAL.DBContext
{
    public class TMSDbContext:DbContext
    {
        public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Companies> Companiess { get; set; }
    }
}
