using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Commend.Models
{
    public class ConfirmEmailModelCommden:IRequest<Response<string>>
    {
        public readonly string token;

        public ConfirmEmailModelCommden(string token)
        {
            this.token = token;
        }

    }
}
