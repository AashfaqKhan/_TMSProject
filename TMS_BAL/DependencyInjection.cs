using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TMS_DAL.DBContext;
using TMS_BAL.Service.IService;
using TMS_BAL.Service;
using Microsoft.AspNetCore.Identity;
using TMS_DAL.Entities;

namespace TMS_BAL
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<TMSDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddIdentity<User, IdentityRole<Guid>>()
            //.AddEntityFrameworkStores<TMSDbContext>()
            //      .AddDefaultTokenProviders();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<PashwordHashService>();
            services.AddScoped<ICompaniesService, CompaniesService>();
        }
    }
}
