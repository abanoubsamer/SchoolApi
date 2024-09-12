using AutoMapper;

using SchoolWep.Core.Features.Students.Commend.Models;
using SchoolWep.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {

        public void UpdateStudentMapping()
        {
          

            CreateMap<Student, UpdataStudentCommend>()
                .ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.Postal_Code, src => src.MapFrom(src => src.Address.Postal_Code))
                 ;

            CreateMap<UpdataStudentCommend, Student>()
        .ForMember(dest => dest.LevelId, opt => opt.Condition(src => src.LevelId > 0))
            .ForMember(dest => dest.DepartmentId, opt => opt.Condition(src => src.DepartmentId > 0));



        }
    }
}
