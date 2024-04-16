using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.Entities;

namespace TMS_DAL.Repositories.IRepositories
{
    public interface IUserRoleRepository
    {
        Task DeleteUserRoleAsync(Guid id);
        Task<List<UserRole>> GetAllUsersRoleAsync();
        Task<UserRole> GetUserRoleByIdAsync(Guid id);
        Task<UserRole> UpdateUserRoleAsync(UserRole userrole);
        Task<UserRole> AddUserRoleAsync(UserRole userrole);
    }
}
