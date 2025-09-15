using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DreamDecode.Application.Dream.Interfaces;
using DreamDecode.Application.Dream.Mappings;
using DreamDecode.Application.Dream.Services;
using DreamDecode.Application.Interpretation.Interfaces;
using DreamDecode.Application.Interpretation.Mappings;
using DreamDecode.Application.Interpretation.Services;
using DreamDecode.Application.User.Interfaces;
using DreamDecode.Application.User.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DreamDecode.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminManagement, AdminManagementService>();
            services.AddScoped<IDreamService, DreamService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IInterpretationService, InterpretationService>();
            services.AddScoped<CurrentUserService>();
           

            return services;
        }
    }
}
