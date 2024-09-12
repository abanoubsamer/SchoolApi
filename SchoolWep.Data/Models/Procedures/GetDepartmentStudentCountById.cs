using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models.Procedures
{
    public class GetDepartmentStudentCountById
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int StudentCount { get; set; }
    }
    public class GetDepartmentStudentCountByIdPrameters
    {
        public int Did { get; set; } = 0;
    }
}
