using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Domain.User.Entities;
using DreamDecode.Domain.User.Factory;
using DreamDecode.Infrastructure.Data;
using DreamDecode.Infrastructure.Factory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DreamDecode.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<AppDbContext>(o =>
                o.UseSqlServer(cfg.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequiredLength = 7;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IJwtFactory, JwtTokenFactory>();

            // ADD JWT AUTHENTICATION BACK HERE:
           

            return services;
        }
    }

}
