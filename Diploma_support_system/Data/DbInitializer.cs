using Diploma_support_system.Models;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_support_system.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == SD.DeanUser)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.DeanUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.PromoterUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.StudentUser)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Marcin",
                Surname = "Cechowski",
                City = "Olsztyn",
                EmailConfirmed = true,
                PhoneNumber = "1112223333"
                
            }, "Admin123*").GetAwaiter().GetResult();

            IdentityUser user = await _db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");

            await _userManager.AddToRoleAsync((ApplicationUser)user, SD.DeanUser);

        }
    }
}
