using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_BAL.Service.IService;
using TMS_DAL.Entities;
using TMS_DAL.Repositories;
using TMS_DAL.Repositories.IRepositories;
using TMS_DTO.DTOs;

namespace TMS_BAL.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository)
        { 
            _userRoleRepository = userRoleRepository;
        }

        public async Task<UserRoleDTO> AddUserRoleAsync(UserRoleDTO userroleDTO)
        {
            try
            {
                var userEntity = new UserRole
                {

                    Name = userroleDTO.Name,
                    IsActive = userroleDTO.IsActive,
                    CompanyId = userroleDTO.CompanyId,
                };
                var adduserrole= await _userRoleRepository.AddUserRoleAsync(userEntity);
                return userroleDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteUserRoleAsync(Guid id)
        {
            try
            {
                await _userRoleRepository.DeleteUserRoleAsync(id);
            }
            catch (Exception )
            {

                throw;
            }
        }

        public async Task<UserRoleDTO> GetUserRoleByIdAsync(Guid id)
        {
            try
            {
                var userroleEntity = await _userRoleRepository.GetUserRoleByIdAsync(id);


                var userroleDTO = new UserRoleDTO
                {
                    Id = userroleEntity.Id,
                    Name = userroleEntity.Name,
                    IsActive = userroleEntity.IsActive,
                    CompanyId = userroleEntity.CompanyId,
                  

                };

                return userroleDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<UserRoleDTO>> GetUsersRoleAsync()
        {
            try
            {
                var listdata = _userRoleRepository.GetAllUsersRoleAsync();
                var userrolemodel = new List<UserRoleDTO>();
                foreach (var userrole in listdata.Result)
                {
                    userrolemodel.Add(new UserRoleDTO()
                    {
                        Id = userrole.Id,
                        Name = userrole.Name,
                        IsActive = userrole.IsActive,
                        CompanyId = userrole.CompanyId,
                        
                      


                    });
                }
                return userrolemodel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserRoleDTO> UpdateUserRoleAsync(UserRoleDTO userroleDTO)
        {
           
            var userroleEntity = new UserRole
            {
                Id = userroleDTO.Id,
                Name = userroleDTO.Name,
                IsActive = userroleDTO.IsActive,
                CompanyId = userroleDTO.CompanyId,
               
            };

            var updatedUserroleEntity = await _userRoleRepository.UpdateUserRoleAsync(userroleEntity);
            return new UserRoleDTO
            {
                Id = updatedUserroleEntity.Id,
                Name = updatedUserroleEntity.Name,
                IsActive = updatedUserroleEntity.IsActive,
                CompanyId = updatedUserroleEntity.CompanyId,
            };
        }
        public async Task<string> GetRoleName(Guid roleId)
        {
            var role = await _userRoleRepository.GetUserRoleByIdAsync(roleId);
            return role?.Name ?? "Unknown Role";
        }

        
    }
}
