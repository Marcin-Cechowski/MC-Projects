using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Models.ViewModels;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Areas.Admin.Controllers
{ 

   // [Authorize(Roles =SD.DeanUser)]
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
       
        [BindProperty]
        public StudentDiplomaViewModel StudentDiplomaVM { get; set; }

        public StudentController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

            StudentDiplomaVM = new StudentDiplomaViewModel()
            {
                Promoter = _db.Promoter,
                Student = new Models.Student()
            };
        }
        public async Task<IActionResult> Index()
        {
            var students = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).ToListAsync();
            return View(students);
        }
        public IActionResult Create()
        {
            return View(StudentDiplomaVM);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            StudentDiplomaVM.Student.GroupId = Convert.ToInt32(Request.Form["GroupId"].ToString());
            if (!ModelState.IsValid)
            {
                return View(StudentDiplomaVM);
            }

            _db.Student.Add(StudentDiplomaVM.Student);
            await _db.SaveChangesAsync();

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var studentFromDb = await _db.Student.FindAsync(StudentDiplomaVM.Student.Id);

            if (files.Count > 0)
            {
                //file been uploaded
                var uploads = Path.Combine(webRootPath, "diplomas");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, StudentDiplomaVM.Student.Name + "_" + StudentDiplomaVM.Student.Surname + extension),
                    FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                studentFromDb.Diploma = StudentDiplomaVM.Student.Name+"_"+ StudentDiplomaVM.Student.Surname + extension;
            }
            else
            {
                //no file uploadede
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult DownloadFile(string filePath, string name, string surname)
        {
            string extension = Path.GetExtension(filePath);
            string webRootPath = _hostingEnvironment.WebRootPath;

            var finalPath = webRootPath + "\\diplomas\\"+ filePath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(finalPath);

            string fileName = $"{name}_{surname}{extension}";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
        [ActionName("GetStudent")]
        public async Task<IActionResult> GetStudent(int id)
        {
           
            List<Models.Student> studentList = new List<Models.Student>();

           

            studentList = (from subStudent in _db.Student
                           where subStudent.PromoterId == id
                           select subStudent).ToList();

            return Json(new SelectList(studentList, "Id", "GroupNumber"));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentDiplomaVM.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            StudentDiplomaVM.Group = await _db.Group.Where(s => s.PromoterId == StudentDiplomaVM.Student.PromoterId)
                .ToListAsync();

            var claimsIndentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);
            StudentDiplomaVM.AppUser = await _db.ApplicationUsers.FirstOrDefaultAsync(m => m.Name == StudentDiplomaVM.Student.Name && m.Surname == StudentDiplomaVM.Student.Surname);


            if (StudentDiplomaVM.Group == null)
            {
                return NotFound();
            }

            return View(StudentDiplomaVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            var claimsIndentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(m => m.Name == StudentDiplomaVM.AppUser.Name && m.Surname == StudentDiplomaVM.AppUser.Surname);
            user.Name = StudentDiplomaVM.Student.Name;
            user.Surname = StudentDiplomaVM.Student.Surname;
            if (id == null)
            {
                return NotFound();
            }
            StudentDiplomaVM.Student.GroupId = Convert.ToInt32(Request.Form["GroupId"].ToString());

            if (!ModelState.IsValid)
            {
                StudentDiplomaVM.Group = await _db.Group.Where(s => s.PromoterId == StudentDiplomaVM.Student.PromoterId).ToListAsync();
                return View(StudentDiplomaVM);
            }

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var studentFromDb = await _db.Student.FindAsync(StudentDiplomaVM.Student.Id);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, "diplomas");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var filePath = Path.Combine(webRootPath, studentFromDb.Diploma.TrimStart('\\'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                //we will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, StudentDiplomaVM.Student.Name + "_" + StudentDiplomaVM.Student.Surname + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                studentFromDb.Diploma = StudentDiplomaVM.Student.Name + "_" + StudentDiplomaVM.Student.Surname + extension_new;
            }

            studentFromDb.Name = StudentDiplomaVM.Student.Name;
            studentFromDb.Surname = StudentDiplomaVM.Student.Surname;
            studentFromDb.Description = StudentDiplomaVM.Student.Description;
            studentFromDb.DiplomaName = StudentDiplomaVM.Student.DiplomaName;
            studentFromDb.PromoterId = StudentDiplomaVM.Student.PromoterId;
            studentFromDb.GroupId = StudentDiplomaVM.Student.GroupId;
            studentFromDb.ExamDate = StudentDiplomaVM.Student.ExamDate;

            if (ModelState.IsValid)
            {
                _db.ApplicationUsers.Update(user);
                await _db.SaveChangesAsync();
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentDiplomaVM.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            StudentDiplomaVM.Group = await _db.Group.Where(s => s.PromoterId == StudentDiplomaVM.Student.PromoterId)
                .ToListAsync();

            if (StudentDiplomaVM.Student == null)
            {
                return NotFound();
            }

            return View(StudentDiplomaVM);
        }
        //POST - Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            return RedirectToAction(nameof(Edit), new { id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentDiplomaVM.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group)
                .SingleOrDefaultAsync(m => m.Id == id);


            if (StudentDiplomaVM.Student == null)
            {
                return NotFound();
            }

            return View(StudentDiplomaVM);
        }
        //Post -delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Models.Student student = await _db.Student.FindAsync(id);



            if (student != null)
            {

                //Delete the original file
                var imagePath = Path.Combine(webRootPath + "\\diplomas\\", student.Diploma.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.Student.Remove(student);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

    }
}
