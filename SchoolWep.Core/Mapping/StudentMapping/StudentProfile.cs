using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.StudentMapping
{
    public partial class StudentProfile:Profile
    {
        public StudentProfile()
        {
            AddStudentMapping();
            GetStudentMapping();
            UpdateStudentMapping();
        }

    }
}
