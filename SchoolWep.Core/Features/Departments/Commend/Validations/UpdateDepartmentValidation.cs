using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Features.Departments.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.DepartmentServices.DbDepartmentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Commend.Validations
{
    public class UpdateDepartmentValidation:AbstractValidator<UpdateDepartmentModelCommend>
    {

        #region Fields
        private readonly IDbDepartmentServices _dbDepartmentServices;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        #endregion


        #region Constructor
        public UpdateDepartmentValidation(IDbDepartmentServices dbDepartmentServices, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _dbDepartmentServices = dbDepartmentServices;
            _StringLocalizer = stringLocalizer;
            ApplyValidationModel();
            ApplyCutomValidationModel();
        }
        #endregion

        #region Function Validation
        public void ApplyValidationModel()
        {


            RuleFor(dep => dep.NameEn)
                .MaximumLength(15).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MaxLength])
                .MinimumLength(3).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MinLength])
                .Matches("^[A-Z][a-z]+$").WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);


            RuleFor(dep => dep.NameAr)
             .MaximumLength(15).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MaxLength])
                .MinimumLength(3).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MinLength]);
               

            RuleFor(dep => dep.Capsity)
                .ExclusiveBetween(49, 8001)
                .WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Range_Between + " [50,8000]"]);



        }

        public void ApplyCutomValidationModel()
        {
            RuleFor(dep => dep.ManagerId).MustAsync(async (key, CancellationToken) => await _dbDepartmentServices.ChackToManagerId(key))
                          .WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Invalid]);
        }

        #endregion
    }
}
