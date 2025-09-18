using DreamDecode.Application.Interpretation.Interfaces;
using DreamDecode.Application.Interpretation.Services;
using DreamDecode.Application.User.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamDecode.API.Controllers
{
    
   // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManagement _adminService;

        public AdminController(IAdminManagement adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> All() {
            var admins =await _adminService.GetAllAdminsAsync();
            var result = admins.Select(a => new
            {
                a.Id,
                a.Email,
                a.UserName
            });
            return Ok(result);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegisterDto dto)
        {
            var result = await _adminService.AddAdminAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAdmin(string email)
        {
            var result = await _adminService.DeleteAdminAsync(email);
            return Ok(result);
        }
    }

}
