using FluentValidation;
using SchoolWep.Core.Features.Students.Commend.Models;
using SchoolWep.Services.StudentServices.DbStudentServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Commend.Validations
{
    public class UpdateStudentValidations : AbstractValidator<UpdataStudentCommend>
    {
        #region Fields
        private readonly IDbStudentServices _StudentServices;
        #endregion
        public UpdateStudentValidations(IDbStudentServices studentServices)
        {
            _StudentServices = studentServices;
            ApplyValidationRule();
            ApplyCutomValidationRule();
        }

        public void ApplyValidationRule()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("is required")
                .NotNull().WithMessage("Not Null");

            // Validation  First Name
            RuleFor(x => x.FirstName)
       
            .MaximumLength(15).WithMessage("Max Length is 15 characters.")
            .MinimumLength(3).WithMessage("Min Length is 3 characters.")
            .Matches("^[A-Z][a-z]+$").WithMessage(" must start with an uppercase letter followed by lowercase letters.");
            // Validation  Last Name
            RuleFor(x => x.LastName)
            
                .MaximumLength(15).WithMessage("Max Length is 15 characters.")
                .MinimumLength(3).WithMessage("Min Length is 3 characters.")
                .Matches("^[A-Z][a-z]+$").WithMessage("must start with an uppercase letter followed by lowercase letters.");

            // Validation  BirthDay
            RuleFor(x => x.BirthDay)
              
            .GreaterThan(DateTime.Parse("1999/1/1")).WithMessage("must Be Greater Than  \"1999/1/1\" !")
             .LessThan(DateTime.UtcNow.AddYears(-18).Date).WithMessage("must be More than 18 years from today.");

            // Validation  Postal_Code
            RuleFor(x => x.Postal_Code).ExclusiveBetween(1000, 9000).WithMessage("Range Between [1000,9000]");
                    
                // Validation  DepartmentId
        



        }
        public void ApplyCutomValidationRule()
        {

            RuleFor(x => x.Id)
            .MustAsync(async (key, CancellationToken) => await _StudentServices.StudentIsExist(key))
            .WithMessage("Student Not Found");

            RuleFor(x => x.FullName)
              .MustAsync(async (model,key, CancellationToken) => !await _StudentServices.IsNameExistAsSelf(key,model.Id))
              .WithMessage("Is Student The Exist To Other");
            
            RuleFor(x => x.DepartmentId)
             .MustAsync(async (key, CancellationToken) => key > 0 ? await _StudentServices.CheckDep(key) : true)
             .WithMessage("Invalid");

            RuleFor(x => x.LevelId)
             .MustAsync(async (key, CancellationToken) => key > 0 ? await _StudentServices.CheckDep(key) : true)
             .WithMessage("Invalid");
        }

    }
}
