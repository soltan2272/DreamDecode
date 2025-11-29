using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.Dream.DTOs;
using DreamDecode.Application.Dream.Interfaces;
using DreamDecode.Domain.Dream.Entities;
using DreamDecode.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamDecode.Application.Dream.Services
{
    public class DreamService : IDreamService
    {
        private readonly AppDbContext _context;
        private readonly CurrentUserService getCurrentUser;
        

        public DreamService(AppDbContext context, CurrentUserService getCurrentUser)
        {
            _context = context;
            this.getCurrentUser = getCurrentUser;
           
        }

       
        public async Task<DreamDto> AddDreamAsync(DreamCreateDto dto, string userId)
        {
            
          
            var dream = new DreamEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId,
                IsPaid = dto.IsPaid
            };

            _context.Dreams.Add(dream);
            await _context.SaveChangesAsync();

            return new DreamDto
            {
                DreamId = dream.DreamId,
                Title = dream.Title,
                Description = dream.Description,
                SubmittedAt = dream.SubmittedAt,
                IsInterpreted = dream.IsInterpreted,
                IsPaid = dream.IsPaid
            };
        }

        public async Task<IEnumerable<DreamDto>> GetUserDreamsAsync(string userId)
        {
            return await _context.Dreams
                .Where(d => d.UserId == userId)
                .Select(d => new DreamDto
                {
                    DreamId = d.DreamId,
                    Title = d.Title,
                    Description = d.Description,
                    SubmittedAt = d.SubmittedAt,
                    IsInterpreted = d.IsInterpreted,
                    InterpretationText=d.InterpretationText,
                    IsPaid = d.IsPaid
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DreamDto>> GetAllDreamsAsync()
        {
            return await _context.Dreams
                .Select(d => new DreamDto
                {
                    DreamId = d.DreamId,
                    Title = d.Title,
                    Description = d.Description,
                    SubmittedAt = d.SubmittedAt,
                    IsInterpreted = d.IsInterpreted,
                    IsPaid = d.IsPaid
                })
                .ToListAsync();
        }
    }
}
