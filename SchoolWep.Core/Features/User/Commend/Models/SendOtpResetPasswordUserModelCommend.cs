using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Models
{
    public class SendOtpResetPasswordUserModelCommend:IRequest<Response<string>>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



    }
}
