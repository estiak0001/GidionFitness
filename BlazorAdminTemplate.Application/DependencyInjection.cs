using BlazorAdminTemplate.Application.DTOs.Authentication;
using BlazorAdminTemplate.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IValidator<RegisterRequestDTO>, RegisterRequestValidator>();
            
            services.AddScoped<IValidator<ChangePasswordRequestDTO>, ChangePasswordRequestValidator>();

            return services;
        }
    }
}
