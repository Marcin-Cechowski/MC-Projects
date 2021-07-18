using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Models.ViewModels;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Areas.Promoter.Controllers
{
    [Area("Promoter")]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;
        [BindProperty]
        public IndexExamViewModel IndexExamVM { get; set; }

        [BindProperty]
        public StudentExamViewModel StudentExamVM { get; set; } 
        public ExamController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;

            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {


            var user = await _userManager.GetUserAsync(User);

            var name = user.Name;

            var surname = user.Surname;

            var fullName = name + " " + surname;

         


            IndexExamViewModel indexExamVM = new IndexExamViewModel()
            {
                Exam = await _db.Exam.Include(m => m.Promoter).Include(m=>m.Student).Include(m=>m.Group).ToListAsync(),
                Promoter = await _db.Promoter.Where(m => m.Name.Equals(name)).ToListAsync()
            };


            return View(indexExamVM);
        }

      
        public async Task<IActionResult> StudentExam(int id)
        {
        
            StudentExamVM = new StudentExamViewModel()
            {
                Exam = await _db.Exam.SingleOrDefaultAsync(m => m.StudentId == id),
                Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).SingleOrDefaultAsync(m => m.Id == id)

            };
            return View(StudentExamVM);
        }
        public async Task<IActionResult> ReviewerIndex()
        {
            var user = await _userManager.GetUserAsync(User);

            var name = user.Name;

            var surname = user.Surname;

            var fullName = name + " " + surname;

            IndexExamViewModel _indexExamVM = new IndexExamViewModel()
            {
                Exam = await _db.Exam.Where(m => m.Reviewer.Equals(fullName)).Include(m => m.Promoter).Include(m => m.Student).Include(m => m.Group).ToListAsync(),
            };
            return View(_indexExamVM);
        }
    }
}
