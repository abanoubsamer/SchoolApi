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
    public class CourseStusentConfigurations : IEntityTypeConfiguration<CourseStusent>
    {
        public void Configure(EntityTypeBuilder<CourseStusent> builder)
        {
           builder.HasKey(e => new { e.StudentId, e.CourseId });
        }
    }
}
