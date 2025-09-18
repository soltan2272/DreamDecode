using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.Interpretation.Interfaces;
using DreamDecode.Application.User.DTOs;
using DreamDecode.Domain.User.Entities;
using DreamDecode.Domain.User.Enums;
using Microsoft.AspNetCore.Identity;

namespace DreamDecode.Application.Interpretation.Services
{
    public class AdminManagementService : IAdminManagement
    {
        private readonly UserManager<ApplicationUser> _users;
        private readonly RoleManager<IdentityRole> _roles;

        public AdminManagementService(UserManager<ApplicationUser> users, RoleManager<IdentityRole> roles)
        {
            _users = users;
            _roles = roles;
        }
         
        public async Task<string> AddAdminAsync(RegisterDto dto)
        {
           
            if (!await _roles.RoleExistsAsync(Roles.Admin.ToString()))
                await _roles.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            var exists = await _users.FindByEmailAsync(dto.Email);
            if (exists != null)
                return "Email already exists.";

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName
            };

            var result = await _users.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            await _users.AddToRoleAsync(user, Roles.Admin.ToString());
            return "Admin registered successfully.";
        }

        public async Task<string> DeleteAdminAsync(string email)
        {
            var user = await _users.FindByEmailAsync(email);
            if (user == null) return "Admin not found.";

            var roles = await _users.GetRolesAsync(user);
            if (!roles.Contains(Roles.Admin.ToString()))
                return "User is not an admin.";

            await _users.DeleteAsync(user);
            return "Admin deleted successfully.";
        }

        public async Task<List<ApplicationUser>> GetAllAdminsAsync()
        {
           var users = _users.Users.ToList();
            var admins = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if(await _users.IsInRoleAsync(user, Roles.Admin.ToString()))
                {
                    admins.Add(user);
                }
            }
            return admins;
        }
    }

}
