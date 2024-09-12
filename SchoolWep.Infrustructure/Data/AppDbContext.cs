using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWep.Data.Models;
using SchoolWep.Data.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SchoolWep.Infrustructure.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {

       

        public AppDbContext(DbContextOptions options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       

        }
      
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<CourseStusent> CourseStusent { get; set; }
        public DbSet<CoursesDepartment> CoursesDepartment { get; set; }
        public DbSet<InstructorCourses> InstructorCourses { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        #region View
        public DbSet<DepartmentView> DepartmentView { get; set; }
        #endregion


    }
}
