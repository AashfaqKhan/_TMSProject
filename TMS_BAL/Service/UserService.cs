using Microsoft.Extensions.Logging;
using TMS_BAL.Service.IService;
using TMS_DAL.Entities;
using TMS_DAL.Repositories.IRepositories;
using TMS_DTO.DTOs;
using TMS_BAL.Service;
using TMS_DAL.Repositories;

namespace TMS_BAL.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly PashwordHashService _passwordHashService;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository, PashwordHashService passwordHashService, IUserRoleRepository userRoleRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<UserDTO> AddUserAsync(UserDTO userDTO)
        {
            try
            {
                var passwordSalt = _passwordHashService.GenerateSalt();

                var hashedPassword = _passwordHashService.HashPassword(userDTO.Password);

                var userEntity = new User
                {

                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = hashedPassword, 
                    PasswordSlat = passwordSalt,
                    GenderId = userDTO.GenderId,

                    RoleId = userDTO.RoleId,
                    UserName = userDTO.Email,
                    DOB = userDTO.DOB,
                    Address1 = userDTO.Address1,
                    Status = userDTO.Status,
                    PhoneNo = userDTO.PhoneNo,
                    CompanyId = userDTO.CompanyId,

                };


                var addedUser = await _userRepository.AddUserAsync(userEntity);
                userDTO.CompanyId = addedUser.CompanyId;
                return userDTO;
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
            }
            catch (Exception )
            {
                
                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid? id)
        {

            try
            {
                var userEntity = await _userRepository.GetUserByIdAsync(id);

                
                var userDTO = new UserDTO
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    PasswordSlat = userEntity.PasswordSlat,
                    GenderId = userEntity.GenderId,
                    RoleId = userEntity.RoleId,
                    UserName = userEntity.UserName,
                    DOB = userEntity.DOB,
                    Address1 = userEntity.Address1,
                    Status = userEntity.Status,
                    PhoneNo = userEntity.PhoneNo,

                };

                return userDTO;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Practice

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            try
            {
                var listdata = await _userRepository.GetAllUsersAsync();
                var model = new List<UserDTO>();

                foreach (var user in listdata)
                {
                    var userDTO = MapUserEntityToDTO(user);
                    userDTO.RoleName = await GetUserRoleName(userDTO.RoleId);
                    model.Add(userDTO);
                }

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUsersAsync");
                throw;
            }
        }

        private async Task<string> GetUserRoleName(Guid roleId)
        {
            var userRole = await _userRoleRepository.GetUserRoleByIdAsync(roleId);
            return userRole?.Name ?? "Admin";
        }

        private UserDTO MapUserEntityToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PasswordSlat = user.PasswordSlat,
                GenderId = user.GenderId,
                RoleId = user.RoleId,
                UserName = user.UserName,
                DOB = user.DOB,
                Address1 = user.Address1,
                Status = user.Status,
                PhoneNo = user.PhoneNo,
            };
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDTO)
        {
            try
            {
                var passwordSalt = _passwordHashService.GenerateSalt();

                var hashedPassword = _passwordHashService.HashPassword(userDTO.Password);

                var userEntity = new User
                {
                    Id = userDTO.Id,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = hashedPassword,
                    PasswordSlat = passwordSalt,
                    GenderId = userDTO.GenderId,
                    RoleId = userDTO.RoleId,
                    UserName = userDTO.Email,
                    DOB = userDTO.DOB,
                    Address1 = userDTO.Address1,
                    Status = userDTO.Status,
                    PhoneNo = userDTO.PhoneNo,
                };

                var updatedUserEntity = await _userRepository.UpdateUserAsync(userEntity);
                return new UserDTO
                {
                    Id = updatedUserEntity.Id,
                    FirstName = updatedUserEntity.FirstName,
                    LastName = updatedUserEntity.LastName,
                    Email = updatedUserEntity.Email,
                    Password = _passwordHashService.EncryptPassword(hashedPassword),
                    PasswordSlat = passwordSalt,
                    GenderId = updatedUserEntity.GenderId,
                    RoleId = updatedUserEntity.RoleId,
                    UserName = updatedUserEntity.UserName,
                    DOB = updatedUserEntity.DOB,
                    Address1 = updatedUserEntity.Address1,
                    Status = updatedUserEntity.Status,
                    PhoneNo = updatedUserEntity.PhoneNo,
                };
            }
            catch (Exception)
            {
               
                throw;
            }
        }
        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);

                if (user != null)
                {
                   
                    var userDTO = new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.Password,
                        PasswordSlat = user.PasswordSlat,
                        GenderId = user.GenderId,
                        RoleId = user.RoleId,
                        UserName = user.Email,
                        DOB = user.DOB,
                        Address1 = user.Address1,
                        Status = user.Status,
                        PhoneNo = user.PhoneNo,

                    };

                    return userDTO;
                }

                return null;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task ChangePasswordAsync(Guid? userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }
            if (user.Password != currentPassword)
            {
                throw new ApplicationException("Invalid current password");
            }
            string hashedNewPassword = _passwordHashService.HashPassword(newPassword);

            user.Password = hashedNewPassword;
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
