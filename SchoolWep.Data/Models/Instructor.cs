using SchoolWep.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Models
{
    public class Instructor: GenericLocalizationEntity
    {

        public int Id { get; set; }
        
        
        public string FirstNameAr { get; set; }
        public string LastNameAr { get; set; }

        
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        


        public DateTime BirthDay { get; set; }
        
        public List<Phones>? Phones { get; set; }

        
        public double Salary { get; set; }

        
        public Address? Address { get; set; }






        /// ///////////////////////





        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Instructors")]
        public virtual Department? Department { get; set; }

        [InverseProperty("InstructorManger")]
        public virtual Department? DepartmentMaanger { get; set; }

      
        
        public int? MangerId { get; set; }
        [ForeignKey(nameof(MangerId))]
        public virtual Instructor? Manger { get; set; }





        [InverseProperty("Manger")]
        public virtual ICollection<Instructor>? Instructors { get; set; }



        public virtual ICollection<InstructorCourses> InstructorCourses { get; set; }
    }
}
