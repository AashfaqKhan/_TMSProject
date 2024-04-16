using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.DBContext;
using TMS_DAL.Entities;
using TMS_DAL.Repositories.IRepositories;

namespace TMS_DAL.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly TMSDbContext _dbContext;
        public UserRoleRepository(TMSDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<UserRole> AddUserRoleAsync(UserRole userrole)
        {
            try
            {
                _dbContext.UserRoles.Add(userrole);
                await _dbContext.SaveChangesAsync();

                return userrole;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteUserRoleAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.UserRoles.FindAsync(id);

                if (user != null)
                {
                    _dbContext.UserRoles.Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<UserRole>> GetAllUsersRoleAsync()
        {
            try
            {
                return await _dbContext.UserRoles.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserRole> GetUserRoleByIdAsync(Guid id)
        {
            try
            {
                var Find = await _dbContext.UserRoles.FindAsync(id);
                return Find;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserRole> UpdateUserRoleAsync(UserRole userrole)
        {
            try
            {
                _dbContext.Entry(userrole).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return userrole;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
