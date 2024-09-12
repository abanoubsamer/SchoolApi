using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolWep.Data.OptionsConfiguration;
using SchoolWep.Infrustructure.Data;
using SchoolWep.Infrustructure.Permission;
using SchoolWep.Infrustructure.UnitOfWork;
using SchoolWep.InfrustructurePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Infrustructure
{
    public static class DbInjection
    {
        public static IServiceCollection AddDbServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<AppDbContext>(Options =>
            Options.UseLazyLoadingProxies().UseSqlServer(Configuration["ConnectionString:Defult"]));

            Services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<AppDbContext>()
                   .AddDefaultTokenProviders();
            // Add Unit Of Work
            IServiceCollection serviceCollection = Services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

            Services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            Services.Configure<JwtOptions>(Configuration.GetSection("JWT"));
            Services.Configure<EmailSetting>(Configuration.GetSection("EmailSetting"));
            Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // hna ana bolh ay t3del 7sl fe ale permission yzhr fe s3tha m4 lazm arstr ale app
                options.ValidationInterval = TimeSpan.Zero;
            });





            return Services;
        }
        public static IServiceCollection AddAuthServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["JWT:Audience"],
                    ValidIssuer = Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
                };

            });


            Services.AddSwaggerGen(c =>
            {
                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT token with 'Bearer ' prefix.",
                });

                // Add Security Requirement for Bearer token
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            return Services;
        }

    
}
}
