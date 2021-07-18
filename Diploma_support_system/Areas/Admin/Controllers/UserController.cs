using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _appUser;

        public UserController(ApplicationDbContext db, UserManager<ApplicationUser> appUser)
        {
            _db = db;
            _appUser = appUser;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIndentity = (ClaimsIdentity) this.User.Identity;
            var claim = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(await _db.ApplicationUsers.Where(u=>u.Id != claim.Value).ToListAsync());
        }

        public async Task<IActionResult> Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(m=>m.Id==id);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnd = DateTime.Now.AddYears(1);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _appUser.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            await _appUser.DeleteAsync(user);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnd = DateTime.Now;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
