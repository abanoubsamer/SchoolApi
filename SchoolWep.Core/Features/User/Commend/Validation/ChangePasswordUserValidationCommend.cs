using FluentValidation;
using SchoolWep.Core.Features.User.Commend.Models;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Validation
{
    public class ChangePasswordUserValidationCommend:AbstractValidator<ChangePasswordUserModelCommend>
    {

        private readonly IUserServices _userServices;

        public ChangePasswordUserValidationCommend(IUserServices userServices)
        {
           _userServices = userServices;
            ApplyValidationModel();
           

        }

        public void ApplyValidationModel()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Not Empty")
                 .NotNull().WithMessage("Requerd");           
            
            RuleFor(x => x.OldPassword)
               .NotEmpty().WithMessage("Not Empty")
                .NotNull().WithMessage("Requerd");
            
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Not Empty")
                .NotNull().WithMessage("Requerd");
            
            RuleFor(x => x.ComparePassword)
               .NotEmpty().WithMessage("Not Empty")
                .NotNull().WithMessage("Requerd")
                .Equal(x=>x.NewPassword).WithMessage("Not Match");
        }
        public void ApplyCustomValidationModel()
        {

        }

    }
}
