using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.ViewModels;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserManager<User> _userManager;
        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #region GetAll
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users= await _userManager.Users.ToListAsync();
            var userVm = users.Select(user => new UserVm
            {
                Id = user.Id,
                UserName = user.UserName,
                Dob = user.Dob,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
            return Ok(userVm);
        }
        #endregion
        #region GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var userVM = new UserVm()
            {
                Id = user.Id,
                UserName = user.UserName,
                Dob = user.Dob,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return Ok(userVM);
        }
        #endregion
        #region Filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetUsersPaging(string filter, int pageIndex, int pageSize)
        {
            var users = _userManager.Users;
            if (!string.IsNullOrEmpty(filter))
            {
                users = users.Where(x => x.Email.Contains(filter) 
                                            || x.UserName.Contains(filter)
                                            || x.PhoneNumber.Contains(filter));
            }
            var totalRecords = await users.CountAsync();
            var items = await users.Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .Select(user => new UserVm() 
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                Dob = user.Dob,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                FirstName = user.FirstName,
                                LastName = user.LastName
                            }).ToListAsync();
            var pagination = new Pagination<UserVm>
            {
                Items = items,
                TotalRecords = totalRecords
            };
            return Ok(pagination);
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserCreateRequest userCreateRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userCreateRequest.Email,
                Dob = userCreateRequest.Dob,
                UserName = userCreateRequest.UserName,
                LastName = userCreateRequest.LastName,
                FirstName = userCreateRequest.FirstName,
                PhoneNumber = userCreateRequest.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, userCreateRequest.Password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, userCreateRequest);
            }
            return BadRequest(result.Errors);
        }
        #endregion
        #region Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromBody]UserCreateRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            user.FirstName = userRequest.FirstName;
            user.LastName = userRequest.LastName;
            user.Dob = userRequest.Dob;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        #endregion
        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                var uservm = new UserVm()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Dob = user.Dob,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return Ok(uservm);
            }
            return BadRequest(result.Errors);
        }
    #endregion
    }
}