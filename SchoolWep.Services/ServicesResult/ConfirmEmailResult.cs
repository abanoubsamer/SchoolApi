using SchoolWep.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Services.ServicesResult
{
    public class ConfirmEmailResult: DbServicesResult
    {
        public  ApplicationUser? User { get; set; }

    }
}
