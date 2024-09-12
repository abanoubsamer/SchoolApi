using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

using SchoolWep.Core.Features.Authorization.Roles.Commend.Models;
using SchoolWep.Core.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authorization.Commend.Validation
{
    public class AddRolesValidationCommend:AbstractValidator<AddRolesModelCommend>
    {

        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddRolesValidationCommend(IStringLocalizer<SharedResource> stringLocalizer, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _stringLocalizer=stringLocalizer;
            ApplyValidationModel();
            ApplyCustomValidationModel();
        }


        public void ApplyValidationModel()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull]);
        }

        public void ApplyCustomValidationModel()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (key, CancellationToken) => !await _roleManager.RoleExistsAsync(key))
                .WithMessage("Name Is Exists");
        }



    }
}
