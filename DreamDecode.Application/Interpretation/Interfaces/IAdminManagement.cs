using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.User.DTOs;

namespace DreamDecode.Application.Interpretation.Interfaces
{
    public interface IAdminManagement
    {
        Task<string> AddAdminAsync(RegisterDto dto);
        Task<string> DeleteAdminAsync(string email);
    }

}
