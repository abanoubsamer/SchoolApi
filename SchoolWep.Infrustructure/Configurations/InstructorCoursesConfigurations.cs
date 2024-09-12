using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolWep.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Infrustructure.Configurations
{
    public class InstructorCoursesConfigurations : IEntityTypeConfiguration<InstructorCourses>
    {
        public void Configure(EntityTypeBuilder<InstructorCourses> builder)
        {
            builder.HasKey(x => new { x.InstructorId , x.CourseId });
        }
    }
}
