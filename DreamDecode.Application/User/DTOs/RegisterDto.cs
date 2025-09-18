using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Domain.User.Enums;

namespace DreamDecode.Application.User.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required] public string FullName { get; set; } = "";
        // at least 6, one upper, one number, one special
        [Required, RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{7,}$",
            ErrorMessage = "Password must be 7+ chars, include uppercase, number, special.")]
        public string Password { get; set; } = "";
        public string? Role { get; set; }

    }

}
