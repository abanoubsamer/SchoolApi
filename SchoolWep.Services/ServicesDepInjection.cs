using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using SchoolWep.Infrustructure.Encryption;
using SchoolWep.Services.AuthenticationServices;
using SchoolWep.Services.AuthorizationServices;
using SchoolWep.Services.AuthorizationServices.CurrentUserServicse;
using SchoolWep.Services.DepartmentServices.DbDepartmentServices;
using SchoolWep.Services.MiddlewareServices;
using SchoolWep.Services.SendEmailServices;
using SchoolWep.Services.StudentServices.DbStudentServices;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services
{
    public static class ServicesDepInjection
    {

        public static IServiceCollection AddServicesInjections(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddTransient<IDbStudentServices, DbStudentServices>();
            services.AddScoped<IUserClaimsService, UserClaimsService>();
            services.AddTransient<IDbDepartmentServices, DbDepartmentServices>();
            services.AddTransient<IAuthenticationServices, AuthenticationServices.AuthenticationServices>();
            services.AddTransient<ISendEmailServices, SendEmailServices.SendEmailServices>();
            services.AddTransient<IUserServices, UserServices.UserServices>();
            services.AddTransient<IAuthorizationServices, AuthorizationServices.AuthorizationServices>();
            services.AddTransient<ICurrentUserServicse, CurrentUserServicse>();
            services.AddDataProtection();

            // تسجيل EncryptionService
            services.AddSingleton<EncryptionService>(provider =>
            {
                var dataProtectionProvider = provider.GetRequiredService<IDataProtectionProvider>();
                return new EncryptionService(dataProtectionProvider);
            });

            return services;
        }

    }
}
