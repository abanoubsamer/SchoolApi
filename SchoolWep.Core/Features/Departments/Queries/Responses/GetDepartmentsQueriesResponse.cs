using SchoolWep.Core.Features.Students.Queries.Responed;
using SchoolWep.Core.Pagination;
using SchoolWep.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Queries.Responses
{
    public class GetDepartmentsQueriesResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Manager { get; set; } 

        public int Capsity { get; set; }


        public  PaginationResult<StudentResponseDep>? StudentList { get; set; }
        public  List<InstructorResponseDep>? InstructorList { get; set; }
        public  List<CoureseResponseDep>? CoureseList { get; set; }


    }
    public class StudentResponseDep
    {
        
        public int Id { get; set; }
        public string FullName { get; set; }
 
    }
    public class InstructorResponseDep
    {

        public int Id { get; set; }
        public string FullName { get; set; }

       
    }
    public class CoureseResponseDep
    {

        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}
