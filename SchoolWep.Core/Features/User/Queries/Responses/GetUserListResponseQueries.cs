﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Features.User.Queries.Responses
{
    public class GetUserListResponseQueries
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public List<string>? Roles{ get; set; }

    }
}
