using SchoolWep.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class Department: GenericLocalizationEntity
    {
        public int Id { get; set; }
        
        public string NameAr { get; set; }
        
        public string NameEn { get; set; }


        public int? InstManger { get; set; }
        [ForeignKey(nameof(InstManger))]
        [InverseProperty("DepartmentMaanger")]
        public virtual Instructor? InstructorManger { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Instructor>? Instructors { get; set; }


        public int Capsity { get; set; }

        public virtual ICollection<Student>? Studnets { get; set; }
     
        public virtual ICollection<CoursesDepartment>? Courses { get; set; }

     
    }
}
