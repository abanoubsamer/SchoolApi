using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class CourseStusent
    {

        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Courses Course { get; set; }


        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
        


    }
}
