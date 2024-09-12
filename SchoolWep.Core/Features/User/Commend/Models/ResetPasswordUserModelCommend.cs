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
    public class ResetPasswordUserModelCommend:IRequest<Response<string>>
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPasswoed { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPasswoed))]
        public string ComparePassword { get; set; }
    }
}
