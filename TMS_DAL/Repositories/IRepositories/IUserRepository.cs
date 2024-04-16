using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.Entities;

namespace TMS_DAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task DeleteUserAsync(Guid id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid? id);
        Task<User> UpdateUserAsync(User user);
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
