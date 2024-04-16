using Microsoft.Extensions.Logging;
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
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _roleRepository;

        private readonly PashwordHashService _passwordHashService;


        public CompaniesService(IUserRoleRepository roleRepository, IUserRepository userRepository, ICompaniesRepository companiesRepository, ILogger<UserService> logger, PashwordHashService passwordHashService)
        {
            _companiesRepository = companiesRepository;
            _passwordHashService = passwordHashService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CompaniesDTO> AddCompanyAsync(CompaniesDTO companyDTO)
        {
            try
            {
                var passwordSalt = _passwordHashService.GenerateSalt();

                var hashedPassword = _passwordHashService.HashPassword(companyDTO.Password);
                var hashedConfirnPassword = _passwordHashService.HashPassword(companyDTO.ConfirmPassword);
                var companyId = Guid.NewGuid();
                var userEntity = new Companies
                {

                    Name = companyDTO.Name,
                    Email = companyDTO.Email,
                    Phone = companyDTO.Phone,
                    Logo = companyDTO.Logo,
                    Address = companyDTO.Address,
                    Status = companyDTO.Status,
                    Id = companyId

                };


                var compnayAdded = await _companiesRepository.AddCompanyAsync(userEntity);
                var user = new User();
                user.FirstName = companyDTO.Name;
                user.LastName = "";
                user.Email = companyDTO.Email;
                user.UserName = companyDTO.Email;
                user.PasswordSlat = passwordSalt;
                user.Password= hashedPassword;
                user.Status = true;
                user.Id = Guid.NewGuid();
                user.CompanyId = companyId;
                user.DOB = DateTime.Now;
               

                await  _userRepository.AddUserAsync(user);

                return companyDTO;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            try
            {
                await _companiesRepository.DeleteCompanyAsync(id);
            }
            catch (Exception )
            {

                throw;
            }
        }

        public async Task<List<CompaniesDTO>> GetCompaniesAsync()
        {
            try
            {
                var listdata = await _companiesRepository.GetAllCompaniesAsync();
                var model = new List<CompaniesDTO>();

                foreach (var company in listdata)
                {
                    model.Add(new CompaniesDTO()
                    {
                        Id = company.Id,
                        Name = company.Name,
                        Email = company.Email,
                        Phone = company.Phone,
                        Logo = company.Logo,
                        Address = company.Address,
                        Status = company.Status,


                    });
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CompaniesDTO> GetCompanyByIdAsync(Guid id)
        {
            try
            {
                var companyEntity = await _companiesRepository.GetCompanyByIdAsync(id);


                var companyDTO = new CompaniesDTO
                {
                    Id = companyEntity.Id,
                    Name = companyEntity.Name,
                    Email = companyEntity.Email,
                    Phone = companyEntity.Phone,
                    Logo = companyEntity.Logo,
                    Address = companyEntity.Address,
                    Status = companyEntity.Status,
            

                };

                return companyDTO;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<CompaniesDTO> UpdateCompanyAsync(CompaniesDTO companyDTO)
        {
            try
            {
                var passwordSalt = _passwordHashService.GenerateSalt();

                // Hash the updated password using the new salt
                var hashedPassword = _passwordHashService.HashPassword(companyDTO.Password);
                var hashedConfirmPassword = _passwordHashService.HashPassword(companyDTO.ConfirmPassword);

                var companyEntity = new Companies
                {
                    Id = companyDTO.Id,
                    Name = companyDTO.Name,
                    Email = companyDTO.Email,
                    Phone = companyDTO.Phone,
                    Logo = companyDTO.Logo,
                    Address = companyDTO.Address,
                    Status = companyDTO.Status,

                    
                };

                var updatedCompanyEntity = await _companiesRepository.UpdateCompanyAsync(companyEntity);
                return new CompaniesDTO
                {
                    Id = updatedCompanyEntity.Id,
                    Name = updatedCompanyEntity.Name,
                    Email = updatedCompanyEntity.Email,
                    Phone = updatedCompanyEntity.Phone,
                    Logo = updatedCompanyEntity.Logo,
                    Address = updatedCompanyEntity.Address,
                    Status = updatedCompanyEntity.Status,
                    Password = _passwordHashService.EncryptPassword(hashedPassword),
                    ConfirmPassword = _passwordHashService.EncryptPassword(hashedConfirmPassword),
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
