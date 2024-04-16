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
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly TMSDbContext _dbcontext;
        public CompaniesRepository(TMSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Companies> AddCompanyAsync(Companies company)
        {
            try
            {
                _dbcontext.Companiess.Add(company);
                await _dbcontext.SaveChangesAsync();

                return company;
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
                var user = await _dbcontext.Companiess.FindAsync(id);

                if (user != null)
                {
                    _dbcontext.Companiess.Remove(user);
                    await _dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Companies>> GetAllCompaniesAsync()
        {
            try
            {
                return await _dbcontext.Companiess.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Companies> GetCompanyByIdAsync(Guid? id)
        {
            try
            {
                var Find = await _dbcontext.Companiess.FindAsync(id);
                return Find;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Companies> UpdateCompanyAsync(Companies company)
        {
            try
            {
                _dbcontext.Entry(company).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                return company;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
