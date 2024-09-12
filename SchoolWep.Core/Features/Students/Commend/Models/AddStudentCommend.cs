using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Commend.Models
{
    public class AddStudentCommend:IRequest<Response<string>>
    {

       
        public string FirstNameAr { get; set; }

        public string LastNameAr { get; set; }

        public string FullNameAr => FirstNameAr + " " + LastNameAr;

        public string FirstNameEn { get; set; }

        public string LastNameEn { get; set; }

        public string FullNameEn => FirstNameEn + " " + LastNameEn;

        public DateTime BirthDay { get; set; }

        public decimal Gpa { get; set; }

        public int LevelId { get; set; }

        public int DepartmentId { get; set; }

        
        public string Country { get; set; }
        
        
        public string City { get; set; }

        public int Postal_Code { get; set; }


    }
}
