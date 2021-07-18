using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Areas.Promoter.Controllers
{
    [Area("Promoter")]
    public class PromoterController : Controller
    {

        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _hostingEnvironment;


        public PromoterController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;

            _userManager = userManager;

            _hostingEnvironment = hostingEnvironment;
        }
        //get
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Students");
        }
        //get-create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var name = currentUser.Name;
            var surname = currentUser.Surname;

            var promoter = await _db.Promoter.Where(m => m.Name.Equals(name)).Where(x => x.Surname.Equals(surname)).ToListAsync();

            if(promoter.Count > 0)
            {
                return RedirectToAction("Index", "Students");
            }

            return View();
        }
        //post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Promoter promoter)
        {
            if (ModelState.IsValid)
            {
                _db.Promoter.Add(promoter);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(promoter);
        }
       

    }
}
