using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DTO.DTOs;

namespace TMS_BAL.Service.IService
{
    public interface ICompaniesService
    {
        Task<CompaniesDTO> AddCompanyAsync(CompaniesDTO companyDTO);
        Task<List<CompaniesDTO>> GetCompaniesAsync();
        Task<CompaniesDTO> GetCompanyByIdAsync(Guid id);
        Task<CompaniesDTO> UpdateCompanyAsync(CompaniesDTO companyDTO);
        Task DeleteCompanyAsync(Guid id);
    }
}
