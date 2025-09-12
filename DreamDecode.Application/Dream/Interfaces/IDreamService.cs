using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.Dream.DTOs;

namespace DreamDecode.Application.Dream.Interfaces
{
    public interface IDreamService
    {
        Task<DreamDto> AddDreamAsync(DreamCreateDto dto, string userId);
        Task<IEnumerable<DreamDto>> GetUserDreamsAsync(string userId);
        Task<IEnumerable<DreamDto>> GetAllDreamsAsync();
    }
}
