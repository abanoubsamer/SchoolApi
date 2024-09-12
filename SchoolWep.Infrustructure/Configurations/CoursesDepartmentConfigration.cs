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
    public class CoursesDepartmentConfigration : IEntityTypeConfiguration<CoursesDepartment>
    {
        public void Configure(EntityTypeBuilder<CoursesDepartment> builder)
        {
            builder.HasKey(e => new { e.DepartmentId, e.CourseId });

            builder.HasOne(Sc => Sc.Course)
                .WithMany(Sd => Sd.Departments)
                  .HasForeignKey(Sc=>Sc.CourseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(Sd => Sd.Department)
                .WithMany(Sc => Sc.Courses)
                .HasForeignKey(Sd=> Sd.DepartmentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
