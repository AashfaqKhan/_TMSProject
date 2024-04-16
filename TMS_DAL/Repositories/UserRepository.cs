using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.DBContext;
using TMS_DAL.Entities;
using TMS_DAL.Repositories.IRepositories;


namespace TMS_DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TMSDbContext _dbContext;
        
        public UserRepository(TMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception )
            {

                throw;
            }
            
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);

                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<User> GetUserByIdAsync(Guid? id)
        {
            try
            {
                var Find= await _dbContext.Users.FindAsync(id);
                return Find;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
