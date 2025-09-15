using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.Dream.DTOs;
using DreamDecode.Application.Interpretation.DTOs;
using DreamDecode.Domain.Dream.Entities;
using DreamDecode.Domain.Interpretation.Entities.DreamDecode.Domain.Entities;

namespace DreamDecode.Application.Interpretation.Interfaces
{
    public interface IInterpretationService
    {
        Task<IEnumerable<PendingDreamsDto>> GetUninterpretedDreamsAsync();
        Task<string> AddInterpretationAsync(AddInterpretationDto dto, string adminId);
        Task<IEnumerable<DreamDto>> GetMyInterpretationsAsync(string adminId);
    

}
}
