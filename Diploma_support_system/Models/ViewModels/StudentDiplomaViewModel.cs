using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models.ViewModels
{
    public class StudentDiplomaViewModel
    {
        public Student Student { get; set; }
        public ApplicationUser AppUser { get; set; }
        public Message Message { get; set; }
        public IEnumerable<Promoter> Promoter { get; set; }
        public IEnumerable<Group> Group { get; set; }
    }
}
