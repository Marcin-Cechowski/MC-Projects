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
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public DiplomaExamViewModel DiplomaExamVM { get; set; }
        [BindProperty]
        public IndexExamViewModel _indexExamVM { get; set; }
        [BindProperty]
        public StudentExamViewModel IndexExamVM { get; set; }

        public ExamController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;

            
        }
        public async Task<IActionResult> Index()
        {
            var exams = await _db.Exam.Include(m => m.Student).Include(m => m.Promoter).Include(m=>m.Group).ToListAsync();

           
            return View(exams);
        }
        public async Task<IActionResult> IndexExams()
        {
            _indexExamVM = new IndexExamViewModel
            {
                Exam= await _db.Exam.Include(m=>m.Student).ToListAsync(),
                Student = await _db.Student.Include(m=>m.Promoter).Include(m=>m.Group).ToListAsync()
            };
            return View(_indexExamVM);
        }
        [ActionName("GetReviewer")]
        public async Task<IActionResult> GetReviewer()
        {

            var deanRoleUsers = await _userManager.GetUsersInRoleAsync(SD.DeanUser);

            var promoterRoleUsers = await _userManager.GetUsersInRoleAsync(SD.PromoterUser);

            var reviewers = deanRoleUsers.Concat(promoterRoleUsers);

            var uniqueUsers = new HashSet<ApplicationUser>(reviewers);

            return Json(new SelectList(uniqueUsers, "Name", "Surname"));
        }
        [ActionName("GetQuestions")]
        public IActionResult GetQuestions()
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var path = webRootPath + "\\Exam_questions\\" + "Questions.txt";
            var questions = System.IO.File.ReadLines(path);


            

            return Json(new SelectList(questions));
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
        public async Task<IActionResult> Create(int id)
        {


            DiplomaExamVM = new DiplomaExamViewModel()
            {
                Exam = new Exam(),
                Promoter = _db.Promoter,
                Group = _db.Group

            };

            
            //DiplomaExamVM.Exam.StudentId = id;
            //DiplomaExamVM.Exam.GroupId = DiplomaExamVM.Exam.Student.GroupId;

            //DiplomaExamVM.Exam.PromoterId = DiplomaExamVM.Exam.Student.PromoterId;

            DiplomaExamVM.Exam.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group)
             .SingleOrDefaultAsync(m => m.Id == id);

            DiplomaExamVM.Group = await _db.Group.Where(s => s.PromoterId == DiplomaExamVM.Exam.Student.PromoterId)
               .ToListAsync();



            var webRootPath = _hostingEnvironment.WebRootPath;
            var path = webRootPath + "\\Exam_questions\\" + "Questions.txt";
            var questions = System.IO.File.ReadLines(path);


            ViewBag.Questions = questions;


            return View(DiplomaExamVM);
        }
        //post-Create
        [HttpPost, ActionName("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiplomaExamViewModel examVM,int id)
        {
           // examVM.Exam.Student.GroupId = Convert.ToInt32(Request.Form["GroupId"].ToString());
            var current = await _userManager.GetUserAsync(User);
            var appUserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                var exam = new Exam
                {
                    StudentId = id,
                    GroupId = examVM.Exam.Student.GroupId,
                    PromoterId = examVM.Exam.Student.PromoterId,
                    AppUserId = appUserId,
                    FirstQuestion = examVM.Exam.FirstQuestion,
                    SecoundQuestion = examVM.Exam.SecoundQuestion,
                    ThirdQuestion = examVM.Exam.ThirdQuestion,
                    ExamDate = examVM.Exam.ExamDate,
                    Reviewer = examVM.Exam.Reviewer


                };

                _db.Exam.Add(exam);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var exam = await _db.Exam.Include(m => m.Id)
            //    .SingleOrDefaultAsync(m => m.Id == id);
        


            DiplomaExamViewModel ExamVM = new DiplomaExamViewModel()
            {
                Exam = await _db.Exam.SingleOrDefaultAsync(m => m.Id == id)
             
            };

            ExamVM.Exam.Student = await _db.Student.Include(m=>m.Promoter).Include(m=>m.Group).SingleOrDefaultAsync(m => m.Id == ExamVM.Exam.StudentId);
            return View(ExamVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
           


            DiplomaExamViewModel ExamVM = new DiplomaExamViewModel()
            {
                Exam = await _db.Exam.SingleOrDefaultAsync(m => m.Id == id),
               
            };
            ExamVM.Exam.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).SingleOrDefaultAsync(m => m.Id == ExamVM.Exam.StudentId);
            return View(ExamVM);
        }
        //Post -delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
           
            Exam exam = await _db.Exam.FindAsync(id);

            if (exam != null)
            {
                _db.Exam.Remove(exam);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> StudentExam(int id)
        {

            IndexExamVM = new StudentExamViewModel()
            {
                Exam = await _db.Exam.SingleOrDefaultAsync(m => m.Id == id),
               

            };
            IndexExamVM.Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).SingleOrDefaultAsync(m => m.Id == IndexExamVM.Exam.StudentId);
            return View(IndexExamVM);

        }
    }
}
