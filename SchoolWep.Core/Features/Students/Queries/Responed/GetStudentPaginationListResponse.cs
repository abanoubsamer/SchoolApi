using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Students.Queries.Responed
{
    public class GetStudentPaginationListResponse: GetStudentListResponed
    {
        [JsonIgnore]
        public DateTime BirthDay { get; set; }

        public GetStudentPaginationListResponse(int id, string fullname
            , DateTime birthDay,
            decimal? gpa,
            string depName,
            string levelName)
        {
            Id = id;
            FullName = fullname;
            BirthDay = birthDay;
            Gpa = gpa;
            Age = DateTime.UtcNow.Year - birthDay.Year;
            Department = depName;
            Level=levelName;
        }
    }


}
