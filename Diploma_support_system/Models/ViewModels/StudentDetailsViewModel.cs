using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models.ViewModels
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Promoter> Promoter { get; set; }
    }
}
