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

namespace Diploma_support_system.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentDiplomaController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public StudentDiplomaViewModel StudentDiplomaVM { get; set; }

        public StudentDiplomaController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;

            _userManager = userManager;

            _hostingEnvironment = hostingEnvironment;
            StudentDiplomaVM = new StudentDiplomaViewModel()
            {
                Promoter = _db.Promoter,
                Student = new Models.Student()
            };
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
            var test = IndexVM.Student.Where(m => m.Name.Equals(name) && m.Surname.Equals(surname)).ToList();
            if (test.Count>0)
            {
                var student = IndexVM.Student.Where(m => m.Name.Equals(name)).Where(m => m.Surname.Equals(surname)).First();
                return RedirectToAction("Details", new { id = student.Id });
            }
            else
            {
                return RedirectToAction("Create");
            }

            return View(IndexVM);
        }
        public IActionResult Create()
        {
            return View(StudentDiplomaVM);
        }
        [HttpPost, ActionName("Create Student")]
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

                using (var fileStream = new FileStream(Path.Combine(uploads, StudentDiplomaVM.Student.DiplomaName + extension),
                    FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                //studentFromDb.Diploma = StudentDiplomaVM.Student.Name+"_"+ StudentDiplomaVM.Student.Surname + extension;
                studentFromDb.Diploma = StudentDiplomaVM.Student.DiplomaName + extension;
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

            var finalPath = webRootPath + "\\diplomas\\" + filePath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(finalPath);

            string fileName = $"{name}_{surname}{extension}";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }
        public async Task<IActionResult> Details(int id)
        {
           
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
        [ActionName("GetGroup")]
        public async Task<IActionResult> GetGroup(int id)
        {
            List<Group> groupList = new List<Group>();

            groupList = (from subGroup in _db.Group
                         where subGroup.PromoterId == id
                         select subGroup).ToList();

            return Json(new SelectList(groupList, "Id", "GroupNumber"));
        }
        [HttpPost, ActionName("Create Message")]
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

            return RedirectToAction("Details", new { id = message.StudentId });

        }
    }
}
