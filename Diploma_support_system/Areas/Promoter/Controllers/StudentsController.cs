using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diploma_support_system.Areas.Promoter.Controllers
{
    [Area("Promoter")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public StudentDiplomaViewModel StudentDiplomaVM { get; set; }

        public StudentsController(ApplicationDbContext db, UserManager<ApplicationUser>userManager, IWebHostEnvironment hostingEnvironment)
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


            IndexViewModel IndexVM = new IndexViewModel()
            {
                Student = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).ToListAsync(),
                Promoter = await _db.Promoter.Where(m => m.Name.Equals(name)).ToListAsync(),
            };
            
            return View(IndexVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            //var studentFromDb = await _db.Student.Include(m => m.Promoter).Include(m => m.Group).Where(m => m.Id == id).FirstOrDefaultAsync();
            //var StudentDiplomaVM = new StudentDiplomaViewModel
            //{
            //    Student = studentFromDb,

            //};
            if (id == default)
            {
                return NotFound();
            }
            StudentDiplomaVM = new StudentDiplomaViewModel()
            {
                Promoter = _db.Promoter,
                Student = new Models.Student(),
                Message = new Message()

            };

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
        [ActionName("GetComments")]
        public async Task<IActionResult> GetComments(int id)
        {
            List<Message> messageList = new List<Message>();

            messageList = (from message in _db.Messages
                           where message.StudentId == id
                           select message).ToList();

            return Json(new SelectList(messageList, "UserName", "Text"));
        }
        [ActionName("GetDates")]
        public async Task<IActionResult> GetDates(int id)
        {
            List<Message> messageList = new List<Message>();

            messageList = (from message in _db.Messages
                           where message.StudentId == id
                           select message).ToList();

            return Json(new SelectList(messageList, "Time", "Text"));
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDiplomaViewModel studetnVm)
        {
            var current = await _userManager.GetUserAsync(User);
          
            // StudentDiplomaVM.Message. = StudentDiplomaVM.Student;
            //StudentDiplomaVM.Message.UserName = current.Email;
            //StudentDiplomaVM.Message.Text = StudentDiplomaVM.Message.Text;
            var appUserId = _userManager.GetUserId(User);

            var message = new Message
            {
                StudentId = studetnVm.Student.Id,
                UserName = current.Email,
                AppUserId = appUserId,
                Text = studetnVm.Message.Text,
                Time = DateTime.Now

            };

            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();
            //var current = await _userManager.GetUserAsync(User);
            //message.Student = StudentDiplomaVM.Student;
            //message.UserName = current.Email;
            //message.Text = StudentDiplomaVM.Message.Text;
            //message.AppUserId = _userManager.GetUserId(User);
            //await _db.Messages.AddAsync(message);
            //await _db.SaveChangesAsync();

            return RedirectToAction("Details",new {id=message.StudentId } );
        }
        public IActionResult DownloadFile(string filePath, string diplomaName)
        {
            string extension = Path.GetExtension(filePath);
            string webRootPath = _hostingEnvironment.WebRootPath;

            var finalPath = webRootPath + "\\diplomas\\" + filePath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(finalPath);

            string fileName = $"{diplomaName}{extension}";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
      
    }
  
}
