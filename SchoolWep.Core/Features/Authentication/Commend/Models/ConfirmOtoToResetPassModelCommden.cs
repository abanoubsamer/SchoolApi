using MediatR;
using SchoolWep.Core.Basics;
using SchoolWep.Core.Features.Authentication.Queries.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Commend.Models
{
    public class ConfirmOtoToResetPassModelCommden:IRequest<Response<string>>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public  string Email { get; set; }
        [Required]
         public string  Otp { get; set; }

    }
}
