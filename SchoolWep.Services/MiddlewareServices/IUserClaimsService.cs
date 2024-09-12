using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.MiddlewareServices
{
    public interface IUserClaimsService
    {
        public Task<bool> ValidateTokenClaimsAsync(string token);

    }
}
