using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Features.User.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Validation
{
    public class UpdateUserValidationCommend:AbstractValidator<UpdateUserModelCommend>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUserServices userServices;

        public UpdateUserValidationCommend(IStringLocalizer<SharedResource> stringLocalizer,IUserServices userServices)
        {
            _stringLocalizer = stringLocalizer;
            this.userServices = userServices;
            ApplyValidationModel();
            ApplyCustomValidationModel();
        }


        public void ApplyValidationModel()
        {
            RuleFor(auth => auth.FirstName)
              .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
              .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull])
              .MaximumLength(15).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MaxLength]+" 15")
              .MinimumLength(3).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MinLength]+ " 3")
              .Matches("^[A-Z][a-z]+$").WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);


            RuleFor(auth => auth.LastName)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Required])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.NotNull])
               .MaximumLength(15).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MaxLength] + " 15")
               .MinimumLength(3).WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.MinLength] + " 3")
               .Matches("^[A-Z][a-z]+$").WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);
        }

        public void ApplyCustomValidationModel()
        {
            RuleFor(user => user.UserName).MustAsync(async (key, CancellationToken) => !await userServices.UserNameIsExsitAsync(key))
                .WithMessage(_stringLocalizer[SharedResourcesKey.Valdiation.UserName_Is_Already_Exist]);
        }
    }
}
