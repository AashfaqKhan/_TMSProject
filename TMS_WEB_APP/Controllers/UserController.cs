using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using TMS_BAL.Service.IService;
using TMS_DAL.Entities;
using TMS_DAL.Repositories;
using TMS_DAL.Repositories.IRepositories;
using TMS_DTO.DTOs;

namespace TMS_WEB_APP.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        private readonly IUserRoleRepository _userRoleRepository;
        private readonly DbContext _dbContext;
        public UserController(IUserService userService, IUserRoleRepository userRoleRepository )
        {
            _userService = userService;
            _userRoleRepository = userRoleRepository;
        }

        // GET
        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return View(users);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        
        // GET
        public async Task<IActionResult> Create()
        {
            var userRoles = await _userRoleRepository.GetAllUsersRoleAsync();

            ViewBag.Roles = userRoles.Select(role => new SelectListItem
            {
                Value = role.Id.ToString(),
                Text = role.Name
            }).ToList();
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            try
            {
                
                var addedUser = await _userService.AddUserAsync(userDTO);
                    return RedirectToAction(nameof(Users));
                
               

            }
            catch (Exception)
            {

                throw;
            }

            
        }

        // GET
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                var userRoles =await _userRoleRepository.GetAllUsersRoleAsync();

                ViewBag.Roles = userRoles.Select(role => new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name,
                    Selected = role.Id == user.RoleId
                }).ToList();

                return View(user);
            }
            catch (Exception)
            {

                throw;
            }
            

            
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UserDTO userDTO)
        {
            try
            {
                if (id != userDTO.Id)
                {
                    return BadRequest();
                }

                    var updatedUser = await _userService.UpdateUserAsync(userDTO);

                    if (updatedUser == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Users));
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }



        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Users)); 
        }
    }
}
