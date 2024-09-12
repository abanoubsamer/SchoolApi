using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.OptionsConfiguration
{
    public class JwtOptions
    {

      
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public int LiveTimeDay { get; set; }
            public string SecretKey { get; set; }
     

    }
}
