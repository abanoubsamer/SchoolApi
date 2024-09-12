using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Features.Authentication.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Commend.Validation
{
    public class RegisterValidationCommend:AbstractValidator<RegisterModelCommend>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUserServices _userServices;
        public RegisterValidationCommend(IUserServices userServices,IStringLocalizer<SharedResource> stringLocalizer)
        {
            _userServices= userServices;

            _stringLocalizer = stringLocalizer;
            ApplyValidationModel();
            ApplyCustomValidationModel();
        }


        public void ApplyValidationModel()
        {
            RuleFor(auth => auth.FirstName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull])
                .MaximumLength(15).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MaxLength])
                .MinimumLength(3).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MinLength])
                .Matches("^[A-Z][a-z]+$").WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);


            RuleFor(auth => auth.LastName)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull])
               .MaximumLength(15).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MaxLength]+" 15")
               .MinimumLength(3).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MinLength]+" 3")
               .Matches("^[A-Z][a-z]+$").WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);

            RuleFor(auth => auth.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull])
             .Matches(@"^[\w\.-]+@[a-zA-Z0-9\.-]+\.[cC][oO][mM]$").WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Invalid]);

            RuleFor(auth => auth.Password)
                .MinimumLength(6).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MinLength]+" 6")
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull]);

            RuleFor(auth => auth.ComparePassword)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull])
               .Equal(auth => auth.Password).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.PasswordsDoNotMatch]);


        }
        public void ApplyCustomValidationModel()
        {
           
        }
    }
}
