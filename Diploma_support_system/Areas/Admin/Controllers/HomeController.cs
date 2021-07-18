using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Models.ViewModels;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;

namespace Diploma_support_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public StudentDiplomaViewModel IndexStudentVM { get; set; }

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            IndexStudentVM = new StudentDiplomaViewModel()
            {
                Promoter = _db.Promoter,
                Student = new Models.Student()
            };

        }
        public async Task<IActionResult> Details(int id)
        {
            //IndexStudentVM = new StudentDiplomaViewModel
            //{
            //    Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).SingleOrDefaultAsync(m => m.Id == id),

            //};
            IndexStudentVM.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group)
            .SingleOrDefaultAsync(m => m.Id == id);
            IndexStudentVM.Group = await _db.Group.Where(s => s.PromoterId == IndexStudentVM.Student.PromoterId)
                .ToListAsync();

            return View(IndexStudentVM);
        }
        public IActionResult DownloadFile(string filePath, string name, string surname)
        {
            string extension = Path.GetExtension(filePath);
            string webRootPath = _hostingEnvironment.WebRootPath;

            var finalPath = webRootPath + "\\diplomas\\" + filePath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(finalPath);

            string fileName = $"{name}_{surname}{extension}";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
    }

}
