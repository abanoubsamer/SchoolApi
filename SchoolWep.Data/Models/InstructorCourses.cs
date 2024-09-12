using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class InstructorCourses
    {

        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Courses Course { get; set; }


        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public virtual Instructor Instructor { get; set; }

    }
}
