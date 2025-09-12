using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.User.DTOs;
using DreamDecode.Application.User.Interfaces;
using DreamDecode.Domain.User.Entities;
using DreamDecode.Domain.User.Enums;
using DreamDecode.Domain.User.Factory;
using Microsoft.AspNetCore.Identity;

namespace DreamDecode.Application.User.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _users;
        private readonly SignInManager<ApplicationUser> _signIn;
        private readonly RoleManager<IdentityRole> _roles;
        private readonly IJwtFactory _jwt;

        public AuthService(UserManager<ApplicationUser> users,
                           SignInManager<ApplicationUser> signIn,
                           RoleManager<IdentityRole> roles,
                           IJwtFactory jwt)
        {
            _users = users;
            _signIn = signIn;
            _roles = roles;
            _jwt = jwt;
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
        {
            var exists = await _users.FindByEmailAsync(dto.Email);
            if (exists != null)
                return new AuthResultDto { Succeeded = false, Errors = new[] { "Email already registered." } };

            if (!await _roles.RoleExistsAsync(dto.Role))
                await _roles.CreateAsync(new IdentityRole(dto.Role));

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName
            };

            var create = await _users.CreateAsync(user, dto.Password);
            if (!create.Succeeded)
                return new AuthResultDto { Succeeded = false, Errors = create.Errors.Select(e => e.Description) };

            await _users.AddToRoleAsync(user, dto.Role);

            var token = _jwt.Create(user, dto.Role);
            return new AuthResultDto { Succeeded = true, Token = token, Role = dto.Role };
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto dto)
        {

            var user = await _users.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new AuthResultDto { Succeeded = false, Errors = new[] { "Invalid credentials." } };
            }


            var ok = await _signIn.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!ok.Succeeded)
            {
                return new AuthResultDto { Succeeded = false, Errors = new[] { "Invalid credentials." } };
            }


            var roles = await _users.GetRolesAsync(user);

            var role = roles.FirstOrDefault();
            if (string.IsNullOrEmpty(role))
            {
                return new AuthResultDto { Succeeded = false, Errors = new[] { "User has no assigned role." } };
            }

            Console.WriteLine($"Using role: {role}");

            var token = _jwt.Create(user, role);

            return new AuthResultDto { Succeeded = true, Token = token, Role = role };
        }
    }
}