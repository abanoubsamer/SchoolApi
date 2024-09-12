using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Queries.Responses;
using SchoolWep.Services.ServicesResult;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Queries.Models
{
    public class LoginModelQueries :IRequest<Response<LoginResponseQueries>>
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
