using DreamDecode.Application.Dream.Interfaces;
using DreamDecode.Application.Interpretation.DTOs;
using DreamDecode.Application.Interpretation.Interfaces;
using DreamDecode.Domain.Dream.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DreamDecode.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] 
    public class InterpretationController : ControllerBase
    {
        private readonly IInterpretationService _interpretationService;

        public InterpretationController(IInterpretationService interpretationService)
        {
            _interpretationService = interpretationService;
        }

        //  1) Dreams to Interpret
        [HttpGet("pending")]
        public async Task<IActionResult> GetUninterpretedDreams()
        {
            var dreams = await _interpretationService.GetUninterpretedDreamsAsync();
            return Ok(dreams);
        }

        //  2) Add Interpretation
        [HttpPost("add")]
        public async Task<IActionResult> AddInterpretation([FromBody] AddInterpretationDto dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            if (string.IsNullOrEmpty(adminId))
                return Unauthorized("Invalid admin token.");

            var result = await _interpretationService.AddInterpretationAsync(dto, adminId);
            return Ok(result);
        }

        //  3) My Interpretations
        [HttpGet("my")]
        public async Task<IActionResult> GetMyInterpretations()
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(adminId))
                return Unauthorized("Invalid admin token.");

            var interpretations = await _interpretationService.GetMyInterpretationsAsync(adminId);
            return Ok(interpretations);
        }
    }
}
