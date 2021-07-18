using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models
{
    public class Promoter
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Promoter Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Promoter Surname")]
        [Required]
        public string Surname { get; set; }

    }
}
