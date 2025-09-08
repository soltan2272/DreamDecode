using DreamDecode.Application.User.DTOs;
using DreamDecode.Application.User.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DreamDecode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var res = await _auth.RegisterAsync(dto);
            return res.Succeeded ? Ok(res) : BadRequest(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var res = await _auth.LoginAsync(dto);
            return res.Succeeded ? Ok(res) : BadRequest(res);
        }
    }
}
