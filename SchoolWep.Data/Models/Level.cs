using SchoolWep.Data.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class Level: GenericLocalizationEntity
    {
        public int Id { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }

    }
}
