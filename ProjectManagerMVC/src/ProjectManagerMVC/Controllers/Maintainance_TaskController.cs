using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagerMVC.Data;
using ProjectManagerMVC.Models.TaskManagerViewModels;

namespace ProjectManagerMVC.Controllers
{
    public class Maintainance_TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Maintainance_TaskController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Maintainance_Task
        public async Task<IActionResult> Index()
        {
            return View(await _context.Maintainance_Task.Include(c => c.Status).Include(c => c.Staff).ToListAsync());
        }

        // GET: Maintainance_Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintainance_Task = await _context.Maintainance_Task.SingleOrDefaultAsync(m => m.ID == id);
            if (maintainance_Task == null)
            {
                return NotFound();
            }

            return View(maintainance_Task);
        }

        // GET: Maintainance_Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maintainance_Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ApprovedComplete,ApprovedDate,CompletionDate,CreationDate,Deadline,Description,IsPriority,Name,Price,StartDate")] Maintainance_Task maintainance_Task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintainance_Task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(maintainance_Task);
        }

        // GET: Maintainance_Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintainance_Task = await _context.Maintainance_Task.SingleOrDefaultAsync(m => m.ID == id);
            if (maintainance_Task == null)
            {
                return NotFound();
            }
            return View(maintainance_Task);
        }

        // POST: Maintainance_Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ApprovedComplete,ApprovedDate,CompletionDate,CreationDate,Deadline,Description,IsPriority,Name,Price,StartDate")] Maintainance_Task maintainance_Task)
        {
            if (id != maintainance_Task.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintainance_Task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Maintainance_TaskExists(maintainance_Task.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(maintainance_Task);
        }

        // GET: Maintainance_Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintainance_Task = await _context.Maintainance_Task.SingleOrDefaultAsync(m => m.ID == id);
            if (maintainance_Task == null)
            {
                return NotFound();
            }

            return View(maintainance_Task);
        }

        // POST: Maintainance_Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintainance_Task = await _context.Maintainance_Task.SingleOrDefaultAsync(m => m.ID == id);
            _context.Maintainance_Task.Remove(maintainance_Task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool Maintainance_TaskExists(int id)
        {
            return _context.Maintainance_Task.Any(e => e.ID == id);
        }
    }
}