using SchoolWep.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class Courses: GenericLocalizationEntity
    {
        public  int  Id { get; set; }

        public string NameAr { get; set; }

        public int Hours { get; set; }

        public string NameEn { get; set; }

        public int LevelId { get; set; }

        [ForeignKey(nameof(LevelId))]
        public virtual Level level { get; set; }

        public virtual ICollection<CoursesDepartment> Departments { get; set; }
        public virtual ICollection<CourseStusent> Students { get; set; }
        public virtual ICollection<InstructorCourses> InstructorCourses { get; set; }



    }
}
