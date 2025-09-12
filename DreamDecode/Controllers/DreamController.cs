using DreamDecode.Application.Dream.DTOs;
using DreamDecode.Application.Dream.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DreamDecode.Application.Dream.Services;

namespace DreamDecode.API.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class DreamController : ControllerBase
    {
        private readonly IDreamService _dreamService;
       

        public DreamController(IDreamService dreamService)
        {
            _dreamService = dreamService;
           
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(DreamCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var dream = await _dreamService.AddDreamAsync(dto,userId);
            return Ok(dream);
        }

        [HttpGet("my-dreams")]
        public async Task<IActionResult> GetUserDreams()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dreams = await _dreamService.GetUserDreamsAsync(userId);
            return Ok(dreams);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var dreams = await _dreamService.GetAllDreamsAsync();
            return Ok(dreams);
        }
    }
}
