using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS_BAL.Service;
using TMS_BAL.Service.IService;
using TMS_DAL.Entities;
using TMS_DTO.DTOs;

namespace TMS_WEB_APP.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompaniesService _companiesService;

        public CompanyController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        public async Task<ActionResult> Companiess()
        {
            try
            {
                var users = await _companiesService.GetCompaniesAsync();
                return View(users);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(CompaniesDTO companiesDTO)
        {
            try
            {

                var addedUser = await _companiesService.AddCompanyAsync(companiesDTO);
                return RedirectToAction("Login", "Account");


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var company = await _companiesService.GetCompanyByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                return View(company);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, CompaniesDTO companiesDTO)
        {
            try
            {
                if (id != companiesDTO.Id)
                {
                    return BadRequest();
                }

                var updatedCompany = await _companiesService.UpdateCompanyAsync(companiesDTO);

                if (updatedCompany == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Companiess));

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _companiesService.DeleteCompanyAsync(id);
            return RedirectToAction(nameof(Companiess));
        }
    }
}
