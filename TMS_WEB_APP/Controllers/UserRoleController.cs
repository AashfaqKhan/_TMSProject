using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS_BAL.Service;
using TMS_BAL.Service.IService;
using TMS_DAL.Entities;
using TMS_DAL.Repositories.IRepositories;
using TMS_DTO.DTOs;

namespace TMS_WEB_APP.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        private IUserRoleService _userroleService;
        private readonly DbContext _dbContext;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userroleService = userRoleService;
            
        }
        // GET
        public async Task<ActionResult> UserRole()
        {
            try
            {
                var usersrole = await _userroleService.GetUsersRoleAsync();
                return View(usersrole);
            }
            catch (Exception)
            {

                throw;
            }
        }
        // GET
        public async Task<ActionResult> Create()
        {
           
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserRoleDTO userroleDTO)
        {
            try
            {
              
                var addedUserRole = await _userroleService.AddUserRoleAsync(userroleDTO);
                return RedirectToAction(nameof(UserRole));

            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var userrole = await _userroleService.GetUserRoleByIdAsync(id);
                if (userrole == null)
                {
                    return NotFound();
                }

                return View(userrole);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, UserRoleDTO userroleDTO)
        {
            try
            {
                if (id != userroleDTO.Id)
                {
                    return BadRequest();
                }

                
                var updatedUserrole = await _userroleService.UpdateUserRoleAsync(userroleDTO);

                if (updatedUserrole == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(UserRole));
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userroleService.DeleteUserRoleAsync(id);
            return RedirectToAction(nameof(UserRole));
        }

    }
}
