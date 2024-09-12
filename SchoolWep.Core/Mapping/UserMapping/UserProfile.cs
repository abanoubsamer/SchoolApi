using AutoMapper;
using SchoolWep.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Mapping.UserMapping
{
    public partial class UserProfile:Profile
    {
       
       
        public UserProfile()
        {
            GetListUserMapping();
            GetUserByIdMapping();
            UpdateUserMapping();
        }
       
    }
}
