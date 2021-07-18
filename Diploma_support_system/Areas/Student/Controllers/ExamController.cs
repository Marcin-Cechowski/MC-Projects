using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Models.ViewModels;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Areas.Student.Controllers
{
    [Area("Student")]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly UserManager<ApplicationUser> _userManager;
        [BindProperty]
        public IndexExamViewModel IndexExamVM { get; set; }

        [BindProperty]
        public StudentExamViewModel StudentExamVM { get; set; }
        public ExamController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {


            var user = await _userManager.GetUserAsync(User);

            var name = user.Name;

            var surname = user.Surname;


            IndexExamViewModel indexExamVM = new IndexExamViewModel()
            {
                Exam = await _db.Exam.Include(m => m.Promoter).Include(m => m.Student).Include(m => m.Group).ToListAsync(),
                Promoter = await _db.Promoter.Where(m => m.Name.Equals(name)).ToListAsync(),

            };

            return View(indexExamVM);
        }
        public async Task<IActionResult> ExamIndex()
        {


            var user = await _userManager.GetUserAsync(User);

            var name = user.Name;

            var surname = user.Surname;

            var studentExamVM = new StudentExamViewModel()
            {
                Student = await _db.Student.SingleOrDefaultAsync(p => (p.Name == name) && (p.Surname == surname)),
                Exam = await _db.Exam.Include(p=>p.Promoter).Include(p=>p.Group).Include(p=>p.Student).SingleOrDefaultAsync(p => (p.Student.Name == name) && (p.Student.Surname == surname))
               
            };
            if(studentExamVM.Exam != null)
            {
                studentExamVM.Student.ExamDate = studentExamVM.Exam.ExamDate;
            }
             
           
              
            return View(studentExamVM);
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
        public IActionResult IndexAfter()
        {
            return View();
        }
        public async Task<IActionResult> StudentExam()
        {
            var user = await _userManager.GetUserAsync(User);

            var name = user.Name;

            var surname = user.Surname;

            StudentExamVM = new StudentExamViewModel()
            {
                Exam = new Exam(),
                Student= new Models.Student()

            };
            StudentExamVM.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).SingleOrDefaultAsync(p=>(p.Name == name) && (p.Surname == surname));
            StudentExamVM.Exam = await _db.Exam.SingleOrDefaultAsync(m => m.StudentId == StudentExamVM.Student.Id);
            return View(StudentExamVM);
        }
        [HttpPost, ActionName("Student Exam")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentExamPost(StudentExamViewModel studentExamVM, int  id)
        {

            StudentExamVM = new StudentExamViewModel()
            {
                Exam = await _db.Exam.SingleOrDefaultAsync(m => m.Id == id)
            };
            StudentExamVM.Exam.FirstAnswer = studentExamVM.Exam.FirstAnswer;
            StudentExamVM.Exam.SecoundAnswer = studentExamVM.Exam.SecoundAnswer;
            StudentExamVM.Exam.ThirdAnswer = studentExamVM.Exam.ThirdAnswer;

            await _db.SaveChangesAsync();


            return RedirectToAction("IndexAfter");
        }
    
    }
}
