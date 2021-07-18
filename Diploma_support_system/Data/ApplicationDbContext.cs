using System;
using System.Collections.Generic;
using System.Text;
using Diploma_support_system.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Promoter> Promoter { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Exam> Exam { get; set; }

    }
}
