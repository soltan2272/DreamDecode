using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDecode.Application.User.DTOs
{
    public class AuthResultDto
    {
        public bool Succeeded { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
