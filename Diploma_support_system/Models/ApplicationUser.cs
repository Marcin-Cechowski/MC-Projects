using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Diploma_support_system.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Messages = new HashSet<Message>();
        }
        public string Name  { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
    
}
