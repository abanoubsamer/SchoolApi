using SchoolWep.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class Student: GenericLocalizationEntity
    {

        public int Id { get; set; }

        public string FirstNameAr { get; set; }

        public string FirstNameEn { get; set; }

        public string LastNameAr { get; set; }

        public string LastNameEn { get; set; }



        public DateTime BirthDay { get; set; }
        
        
        public decimal Gpa { get; set; }
      
        
        public  int LevelId  { get; set; }
        
        
        [ForeignKey(nameof(LevelId))]
        public virtual Level level { get; set; }
        
        
        public int DepartmentId { get; set; }
        
        
        [ForeignKey(nameof(DepartmentId))]
        public virtual Department Department { get; set; }


        public Address Address { get; set; }

        public virtual ICollection<CourseStusent> Courses { get; set; }

    }
}
