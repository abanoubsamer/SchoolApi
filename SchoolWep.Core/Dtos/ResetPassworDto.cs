using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Dtos
{
    public class ResetPassworDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPasswoed { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPasswoed))]
        public string ComparePassword { get; set; }
    }
}
