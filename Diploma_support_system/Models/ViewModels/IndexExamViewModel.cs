using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models.ViewModels
{
    public class IndexExamViewModel
    {
       public IEnumerable<Exam> Exam { get; set; }
        public IEnumerable<Promoter> Promoter { get; set; }
        public IEnumerable<Student> Student { get; set; }
       


    }
}
