using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.DBContext;
using TMS_DAL.Entities;
using TMS_DAL.Repositories;
using TMS_DAL.Repositories.IRepositories;



namespace TMS_DAL
{
    public static class DependancyInjection
    {
        public static void RegisterDALDepedancies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TMSDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            //services.AddIdentity<User, IdentityRole<Guid>>()
            //     .AddEntityFrameworkStores<TMSDbContext>()
            //        .AddDefaultTokenProviders();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<ICompaniesRepository, CompaniesRepository>();

        }

    }
}
