using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DTO.DTOs;

namespace TMS_BAL.Service.IService
{
    public interface IUserRoleService
    {
        Task<UserRoleDTO> AddUserRoleAsync(UserRoleDTO userroleDTO);
        Task<List<UserRoleDTO>> GetUsersRoleAsync();
        Task<UserRoleDTO> GetUserRoleByIdAsync(Guid id);
        Task<UserRoleDTO> UpdateUserRoleAsync(UserRoleDTO userroleDTO);
        Task DeleteUserRoleAsync(Guid id);
        Task<string> GetRoleName(Guid roleId);
    }
}
