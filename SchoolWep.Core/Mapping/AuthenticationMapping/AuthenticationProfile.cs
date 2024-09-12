using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.AuthenticationMapping
{
    public partial class  AuthenticationProfile:Profile
    {

        public AuthenticationProfile()
        {
            RegisterMapping();
            LoginResponseQueriesMapping();
        }

    }
}
