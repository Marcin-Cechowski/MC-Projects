using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Group Number")]
        [Required]
        public string GroupNumber { get; set; }

        [Required]
        [Display(Name = "Promoter")]
        public int PromoterId { get; set; }

        [ForeignKey("PromoterId")]
        public virtual Promoter Promoter { get; set; }

    }
}
