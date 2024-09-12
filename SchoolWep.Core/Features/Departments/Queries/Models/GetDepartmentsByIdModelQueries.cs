using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Departments.Queries.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentsByIdModelQueries:IRequest<Response<GetDepartmentsByIdQueriesResponse>>
    {

        [Required]
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

       
    }
}
