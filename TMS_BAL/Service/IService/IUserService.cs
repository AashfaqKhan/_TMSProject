using System;
using TMS_DTO.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_BAL.Service.IService
{
    public interface IUserService
    {
        Task<UserDTO> AddUserAsync(UserDTO userDTO);
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid? id);
        Task<UserDTO> UpdateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(Guid id);
        Task<UserDTO> GetUserByEmailAsync(string email);

        Task ChangePasswordAsync(Guid? userId, string currentPassword, string newPassword);
    }
}

