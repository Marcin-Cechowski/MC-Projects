using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models
{
    public class Message
    {
      
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }

        public DateTime Time { get; set; }
        [ForeignKey("AppUserId")]
        public virtual ApplicationUser AppUser { get; set; }

        [Display(Name = "AppUser")]
        public string AppUserId { get; set; }


        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [Display(Name = "Student")]
        public int StudentId { get; set; }



    }
}
