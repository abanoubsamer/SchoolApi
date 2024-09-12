using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Queries.Responses
{
    public class LoginResponseQueries
    {
        public string Token { get; set; }
    
        public DateTime Expiration { get; set; }
      
        public string Username { get; set; }
      
        public string UserID { get; set; }
      
        public List<string> Roles { get; set; }

    }
}
