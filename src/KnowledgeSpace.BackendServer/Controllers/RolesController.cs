using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeSpace.ViewModels;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeSpace.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class RolesController : ControllerBase
    {
        public readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        #region GetAll
        //URL: GET: http://localhost:5001/api/roles/
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleVms = roles.Select(r => new RoleCreateRequest()
            {
                Id = r.Id,
                Name = r.Name
            });
            return Ok(roleVms);
        }
        #endregion
        #region GetById
        //URL: GET: http://localhost:5001/api/roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var roleVm = new RoleCreateRequest
            {
                Id = role.Id,
                Name = role.Name
            };
            return Ok(roleVm);
        }
        #endregion
        #region Search
        //URL: GET: http://localhost:5001/api/roles/?filter={filter}&pageIndex=1&pageSize=10
        [HttpGet("filter")]
        public async Task<IActionResult> GetRoles(string filter, int pageIndex, int pageSize)
        {
            var listRoles = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                listRoles = listRoles.Where(r => r.Id.Contains(filter) || r.Name.Contains(filter));
            }
            var totalRecords = await listRoles.CountAsync();
            var items = await listRoles.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Select(r => new RoleCreateRequest()
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();
            var pagination = new Pagination<RoleCreateRequest>
            {
                Items = items,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }
        #endregion
        #region Create
        //URL: POST: http://localhost:5001/api/roles
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleCreateRequest roleVm)
        {
            var role = new IdentityRole
            {
                Id = roleVm.Id,
                Name = roleVm.Name,
                NormalizedName = roleVm.Name.ToUpper()
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, roleVm);
            }
            return BadRequest(result.Errors);
        }
        #endregion
        #region Update
        //URL: PUT: http://localhost:50001/api/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id,[FromBody] RoleCreateRequest roleVm)
        {
            if (id != roleVm.Id)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            role.Name = roleVm.Name;
            role.NormalizedName = roleVm.Name.ToLower();
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        #endregion
        #region Delete
        //URL: Delete: http://localhost:50001/api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                var rolevm = new RoleCreateRequest()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return Ok(rolevm);
            }
            return BadRequest(result.Errors);
        }
        #endregion

    }
}