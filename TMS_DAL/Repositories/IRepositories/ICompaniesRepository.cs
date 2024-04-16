using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DAL.Entities;

namespace TMS_DAL.Repositories.IRepositories
{
    public interface ICompaniesRepository
    {
        Task DeleteCompanyAsync(Guid id);
        Task<List<Companies>> GetAllCompaniesAsync();
        Task<Companies> GetCompanyByIdAsync(Guid? id);
        Task<Companies> UpdateCompanyAsync(Companies company);
        Task<Companies> AddCompanyAsync(Companies company);
    }
}
