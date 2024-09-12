using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    [Owned]
    public class Address
    {

        public string Country { get; set; }
        public string City { get; set; }
        public int Postal_Code	 { get; set; }

    }
}
