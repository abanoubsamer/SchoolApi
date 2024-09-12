using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.Features.Students.Commend.Models;
using SchoolWep.Core.SharedResources;
using SchoolWep.Services.StudentServices.DbStudentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Commend.Validations
{
    public class AddStudentValidations:AbstractValidator<AddStudentCommend>
    {

        #region Fields
        private readonly IDbStudentServices _StudentServices;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;
        #endregion


        #region Constructor
        public AddStudentValidations(IDbStudentServices studentServices,IStringLocalizer<SharedResource> stringLocalizer)
        {
            _StudentServices = studentServices;
            _StringLocalizer = stringLocalizer;
            ApplyValidationRoles();
            ApplyCustemValidationRoles();
        }
        #endregion


        #region Functions Validation
        public void ApplyValidationRoles()
        {
            // Validation  First Name
            RuleFor(x => x.FirstNameAr)
            .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
            .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull])
            .MaximumLength(15).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MaxLength] + "15")
            .MinimumLength(3).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MinLength] + "3");

            // Validation  Last Name
            RuleFor(x => x.LastNameAr)
                .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull])
                .MaximumLength(15).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MaxLength] + "15")
                .MinimumLength(3).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MinLength] + "3");
              

            // Validation  First Name
            RuleFor(x => x.FirstNameEn)
            .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
            .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull])
            .MaximumLength(15).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MaxLength] + "15")
            .MinimumLength(3).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MinLength] + "3")
            .Matches("^[A-Z][a-z]+$").WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);
            // Validation  Last Name
            RuleFor(x => x.LastNameEn)
                .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull])
                .MaximumLength(15).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MaxLength] + "15")
                .MinimumLength(3).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.MinLength] + "3")
                .Matches("^[A-Z][a-z]+$").WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Must_Start_With_uppercase_Letter]);

            // Validation  BirthDay
            RuleFor(x => x.BirthDay).NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull])
               .GreaterThan(DateTime.Parse("1999/1/1")).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Must_Be_More_Than] + " \"1999/1/1\" !")
               .LessThan(DateTime.UtcNow.AddYears(-18).Date).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Must_Be_More_Than_18_Years]);

            // Validation  Postal_Code
            RuleFor(x => x.Postal_Code).ExclusiveBetween(1000, 9000).WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Range_Between] + "[1000,9000]")
                .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull]);
            // Validation  DepartmentId
            RuleFor(x => x.DepartmentId)
                  .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull]);
            // Validation  LevelId
            RuleFor(x => x.LevelId)
                .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull]);

            // Validation  City
            RuleFor(x => x.City)
                .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull]);
            // Validation  Country
            RuleFor(x => x.Country)
               .NotEmpty().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Required])
                .NotNull().WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.NotNull]);

        }


        public void ApplyCustemValidationRoles()
        {
            RuleFor(x => x.FullNameAr)
                .MustAsync(async (model,key, CancellationToken) =>  !await _StudentServices.StudentIsExistName(model.FullNameAr, model.FullNameEn))
                .WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Student_Is_Exist]);


            RuleFor(x => x.DepartmentId)
             .MustAsync(async (key, CancellationToken) => await _StudentServices.CheckDep(key))
             .WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Invalid]);

            RuleFor(x => x.LevelId)
             .MustAsync(async (key, CancellationToken) => await _StudentServices.CheckLevel(key))
             .WithMessage(_StringLocalizer[SharedResourcesKey.Valdiation.Invalid]);
        }
        #endregion

    }
}
