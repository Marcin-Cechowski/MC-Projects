using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diploma_support_system.Data;
using Diploma_support_system.Models;
using Diploma_support_system.Models.ViewModels;
using Diploma_support_system.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Diploma_support_system.Areas.Admin.Controllers
{
    [Authorize(Roles =SD.DeanUser)]

    [Area("Admin")]
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _db;
        [TempData]
        public string StatusMessage { get; set; }
        public GroupController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Get INDEX
        public async Task<IActionResult> Index()
        {
            var group = await _db.Group.Include(s => s.Promoter).ToListAsync();

            return View(group);
        }
        //Get - Create
        public async Task<IActionResult> Create()
        {
            PromoterAndGroupViewModel model = new PromoterAndGroupViewModel()
            {
                PromoterList = await _db.Promoter.ToListAsync(),
                Group = new Group(),
                GroupList = await _db.Group.OrderBy(p => p.GroupNumber).Select(p => p.GroupNumber).Distinct().ToListAsync()
            };
            return View(model);
        }
        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromoterAndGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExists = _db.Group.Include(s => s.Promoter).Where(s =>
                    s.GroupNumber == model.Group.GroupNumber && s.Promoter.Id == model.Group.PromoterId);

                if (doesSubCategoryExists.Count() > 0)
                {
                    StatusMessage = "Error : Group exists under" + doesSubCategoryExists.First().Promoter.Name +
                                    "category. Please use valid group name ";
                }
                else
                {
                    _db.Group.Add(model.Group);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            PromoterAndGroupViewModel modelVM = new PromoterAndGroupViewModel()
            {
                PromoterList = await _db.Promoter.ToListAsync(),
                Group = model.Group,
                GroupList = await _db.Group.OrderBy(p => p.GroupNumber).Select(p => p.GroupNumber).ToListAsync(),
                ErrorMessage = StatusMessage
            };
            return View(modelVM);
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
        //Get - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _db.Group.SingleOrDefaultAsync(m => m.Id == id);

            if (group == null)
            {
                return NotFound();
            }


            PromoterAndGroupViewModel model = new PromoterAndGroupViewModel()
            {
                PromoterList = await _db.Promoter.ToListAsync(),
                Group = group,
                GroupList = await _db.Group.OrderBy(p => p.GroupNumber).Select(p => p.GroupNumber).Distinct().ToListAsync()
            };
            return View(model);
        }
        //Post-Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PromoterAndGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExists = _db.Group.Include(s => s.Promoter).Where(s =>
                    s.GroupNumber == model.Group.GroupNumber && s.Promoter.Id == model.Group.PromoterId);

                if (doesSubCategoryExists.Count() > 0)
                {
                    StatusMessage = "Error : Sub Category exists under" + doesSubCategoryExists.First().Promoter.Name +
                                    "category. Please use valid subcategory name ";
                }
                else
                {
                    var groupFromDb = await _db.Group.FindAsync(model.Group.Id);

                    groupFromDb.GroupNumber = model.Group.GroupNumber;

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            PromoterAndGroupViewModel modelVM = new PromoterAndGroupViewModel()
            {
                PromoterList = await _db.Promoter.ToListAsync(),
                Group = model.Group,
                GroupList = await _db.Group.OrderBy(p => p.GroupNumber).Select(p => p.GroupNumber).ToListAsync(),
                ErrorMessage = StatusMessage
            };
            modelVM.Group.Id = id;
            return View(modelVM);
        }
        //Get Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _db.Group.SingleOrDefaultAsync(m => m.Id == id);

            if (group == null)
            {
                return NotFound();
            }


            PromoterAndGroupViewModel model = new PromoterAndGroupViewModel()
            {
                PromoterList = await _db.Promoter.ToListAsync(),
                Group = group,
                GroupList = await _db.Group.OrderBy(p => p.GroupNumber).Select(p => p.GroupNumber).Distinct().ToListAsync()
            };
            return View(model);
        }
        //POST - Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            return RedirectToAction(nameof(Edit), new { id });
        }
        //Get - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var group = await _db.Group.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }
        //Post -delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var promoter = await _db.Group.FindAsync(id);


            _db.Group.Remove(promoter);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
