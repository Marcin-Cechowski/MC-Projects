using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Models.ViewModels
{
    public class PromoterAndGroupViewModel
    {
        public IEnumerable<Promoter> PromoterList { get; set; }
        public Group Group { get; set; }
        public List<string> GroupList { get; set; }
        public string ErrorMessage { get; set; }
    }
}
