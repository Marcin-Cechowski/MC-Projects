using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string DiplomaName { get; set; }
        public string Description { get; set; } 
        public string ExamDate { get; set; }

        public string Diploma { get; set; }
        [Display(Name = "Promoter")]
        public int PromoterId { get; set; }
        [ForeignKey("PromoterId")]
        public virtual Promoter Promoter { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
      
    }
}
