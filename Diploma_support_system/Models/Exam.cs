using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models
{
    public class Exam
    {
        public int Id { get; set; }

        public string FirstQuestion { get; set; }

        public string FirstAnswer { get; set; }

        public string SecoundQuestion { get; set; }

        public string SecoundAnswer { get; set; }
        public string ThirdQuestion { get; set; }
        public string ExamDate { get; set; }
        public string ThirdAnswer { get; set; }
        public string Reviewer { get; set; }

        [Display(Name = "Promoter")]
        public int PromoterId { get; set; }
        [ForeignKey("PromoterId")]
        public virtual Promoter Promoter { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual ApplicationUser AppUser { get; set; }

        [Display(Name = "AppUser")]
        public string AppUserId { get; set; }


    }
}
