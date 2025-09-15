using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.Dream.DTOs;
using DreamDecode.Application.Interpretation.DTOs;
using DreamDecode.Application.Interpretation.Interfaces;
using DreamDecode.Domain.Dream.Entities;
using DreamDecode.Domain.Interpretation.Entities.DreamDecode.Domain.Entities;
using DreamDecode.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DreamDecode.Application.Interpretation.Services
{
    public class InterpretationService : IInterpretationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public InterpretationService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // 1) Dreams to Interpret
        public async Task<IEnumerable<PendingDreamsDto>> GetUninterpretedDreamsAsync()
        {
            IEnumerable<DreamEntity> dream = await _context.Dreams
                .Where(d => !d.IsInterpreted)
                .Include(d => d.User)
                .ToListAsync();

            return mapper.Map<IEnumerable<PendingDreamsDto>>(dream);
        }

        // 2) Add Interpretation
        public async Task<string> AddInterpretationAsync(AddInterpretationDto dto, string adminId)
        {
            var interpretation = new InterpretationEntity
            {
                DreamId = dto.DreamId,
                AdminId = adminId,
                InterpretationText = dto.InterpretationText,
                InterpretedAt = DateTime.UtcNow
            };

            _context.Interpretations.Add(interpretation);

            // Mark Dream as Interpreted
            var dream = await _context.Dreams.FindAsync(dto.DreamId);
            if (dream != null)
            {
                dream.IsInterpreted = true;
            }

            await _context.SaveChangesAsync();
            return "Interpretation Added Successfully";
        }

        // 3) My Interpretations
        public async Task<IEnumerable<DreamDto>> GetMyInterpretationsAsync(string adminId)
        {
            IEnumerable<InterpretationEntity> interpretations= await _context.Interpretations
                .Where(i => i.AdminId == adminId)
                .Include(i => i.Dream)
                .ToListAsync();

            return mapper.Map<IEnumerable<DreamDto>>(interpretations);
        }
    }
}
