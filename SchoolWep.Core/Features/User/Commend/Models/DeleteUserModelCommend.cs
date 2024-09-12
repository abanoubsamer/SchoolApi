using MediatR;
using SchoolWep.Core.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Commend.Models
{
    public class DeleteUserModelCommend:IRequest<Response<string>>
    {
        public string Id { get; set; }

        public DeleteUserModelCommend(string id)
        {
            Id = id;
        }


    }
}
