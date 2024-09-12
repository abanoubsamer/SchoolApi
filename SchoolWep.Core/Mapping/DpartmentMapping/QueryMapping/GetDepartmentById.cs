using SchoolWep.Core.Features.Departments.Queries.Responses;
using SchoolWep.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.DpartmentMapping
{
    public partial class DepartmentProfile
    {

        public void GetDepartmentById()
        {
            CreateMap<Department, GetDepartmentsByIdQueriesResponse>()
                   .ForMember(des => des.Manager, src =>
                  src.MapFrom(src => src.InstructorManger.Localize(src.InstructorManger.FirstNameAr, src.InstructorManger.FirstNameEn)
                  + " " + src.InstructorManger.Localize(src.InstructorManger.LastNameAr, src.InstructorManger.LastNameEn)))
                  .ForMember(des => des.Name, src => src.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                  .ForMember(des=>des.InstructorList,src=>src.MapFrom(src=>src.Instructors))
                  .ForMember(des=>des.CoureseList,src=>src.MapFrom(src=>src.Courses));

            CreateMap<Student, StudentResponseDep>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // تعيين خصائص فردية
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Localize(src.FirstNameAr, src.FirstNameEn)
                + " " + src.Localize(src.LastNameAr, src.LastNameEn))); // تعيين خصائص فردية



            CreateMap<Instructor, InstructorResponseDep>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // تعيين خصائص فردية
                  .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Localize(src.FirstNameAr,src.FirstNameEn)
                  +" "+src.Localize(src.LastNameAr,src.LastNameEn))); // تعيين خصائص فردية


            CreateMap<Courses, CoureseResponseDep>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // تعيين خصائص فردية
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameAr)));
        }

    }
}
