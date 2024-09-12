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
    public class DeleteStudentValidation:AbstractValidator<DeleteStudentCommend>
    {

        #region Fields
        private readonly IDbStudentServices _StudentServices;
        #endregion


        #region Consturctor
        public DeleteStudentValidation()
        {
            ApplyValidationRule();
           // ApplyCutomValidationRule();
        }
        #endregion

        #region Function Validation

        public void ApplyValidationRule()
        {

            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Must Be Greater Than Or Equal 1")
                .NotEmpty().WithMessage("Shoude Be Not Empty")
                .NotNull().WithMessage("Shoude Be Not Null");

        }
        public void ApplyCutomValidationRule()
        {

        }


        #endregion




    }
}
