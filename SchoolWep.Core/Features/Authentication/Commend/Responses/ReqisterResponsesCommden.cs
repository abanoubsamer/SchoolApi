﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.Authentication.Commend.Responses
{
    public class ReqisterResponsesCommden
    {
        public string Messgage { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
       
        public string Token { get; set; }
        // public DateTime Expiration { get; set; }
        public List<string> Roles { get; set; }
        
        public bool IsAuthenticated { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }


        public DateTime RefreshTokenExpiration { get; set; }
    }
}
