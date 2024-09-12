using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolWep.Core.Behavior;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.SharedResources;
using SchoolWep.Core.Mapping.UserMapping.Resolver;

namespace SchoolWep.Core
{
    public static class ModelCoreDependencies
    {

        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
            {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           
            services.AddScoped<RolesResolver>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehiveor<,>));

         
            return services;
        }
       
    }
}
