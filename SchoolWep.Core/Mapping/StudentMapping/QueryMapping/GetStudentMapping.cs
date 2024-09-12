using AutoMapper;
using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Data.Models;


namespace SchoolWep.Core.Mapping.StudentMapping;
    public partial class StudentProfile 
    {
        public void GetStudentMapping()
        {
         

            CreateMap<Student, GetStudentListResponed>()
                .ForMember(dest => dest.Level, src => src.MapFrom(src => src.level.Localize(src.level.NameAr, src.level.NameEn)))
                 .ForMember(dest => dest.Department, src => src.MapFrom(src => src.Department.Localize(src.Department.NameAr,src.Department.NameEn)))
                   .ForMember(dest => dest.Age, src => src.MapFrom(src => DateTime.UtcNow.Year - src.BirthDay.Year))
                    .ForMember(dest => dest.FullName, src => src.MapFrom(src =>src.Localize(src.FirstNameAr + " " + src.LastNameAr, src.FirstNameEn + " " + src.LastNameEn)))
                .ReverseMap();



        CreateMap<Student, GetSingelStudentResponse>()
             .ForMember(dest => dest.Level, src => src.MapFrom(src => src.level.Localize(src.level.NameAr, src.level.NameEn)))
              .ForMember(dest => dest.Department, src => src.MapFrom(src => src.Department.Localize(src.Department.NameAr, src.Department.NameEn)))
                .ForMember(dest => dest.Age, src => src.MapFrom(src => DateTime.UtcNow.Year - src.BirthDay.Year))
                 .ForMember(dest => dest.FullName, src => src.MapFrom(src => src.Localize(src.FirstNameAr + " " + src.LastNameAr, src.FirstNameEn + " " + src.LastNameEn)))
             .ReverseMap();
    }
    }
