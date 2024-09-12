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

        public void AddStudentMapping()
        {
            CreateMap<Student, AddStudentCommend>().ForMember(dest => dest.City, src => src.MapFrom(src => src.Address.City))
              .ForMember(dest => dest.Country, src => src.MapFrom(src => src.Address.Country))
              .ForMember(dest => dest.Postal_Code, src => src.MapFrom(src => src.Address.Postal_Code))
              .ReverseMap();
        }

    }
}
