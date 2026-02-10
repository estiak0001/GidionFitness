using BlazorAdminTemplate.Application.Interfaces;
using BlazorAdminTemplate.Infrastructure.Authentication;
using BlazorAdminTemplate.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IOrganisationService, OrganisationService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IPaymentGroupService, PaymentGroupService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMemberCardService, MemberCardService>();
            services.AddScoped<IMemberMembershipService, MemberMembershipService>();
            services.AddScoped<IMemberPaymentService, MemberPaymentService>();
            services.AddScoped<IAccessLogsService, AccessLogsService>();
            services.AddScoped<ITrainingClassesService, TrainingClassesService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IStaffManagementService, StaffManagementService>();
            services.AddScoped<IMemberContractService, MemberContractService>();
            services.AddScoped<IClassLocationService, ClassLocationService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IAccessControlService, AccessControlService>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


            return services;
        }
    }
}
