using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineExamManagement.Models.Db;

namespace OnlineExamManagement.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ExamContext _context;

        public TopicsController(ExamContext context)
        {
            _context = context;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            var examContext = _context.Topics.Include(t => t.SubjectCodeNavigation);
            return View(await examContext.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topics = await _context.Topics
                .Include(t => t.SubjectCodeNavigation)
                .FirstOrDefaultAsync(m => m.TopicsCode == id);
            if (topics == null)
            {
                return NotFound();
            }

            return View(topics);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode");
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicsCode,TopicsName,SubjectCode")] Topics topics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode", topics.SubjectCode);
            return View(topics);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topics = await _context.Topics.FindAsync(id);
            if (topics == null)
            {
                return NotFound();
            }
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode", topics.SubjectCode);
            return View(topics);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TopicsCode,TopicsName,SubjectCode")] Topics topics)
        {
            if (id != topics.TopicsCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicsExists(topics.TopicsCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectCode"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectCode", topics.SubjectCode);
            return View(topics);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topics = await _context.Topics
                .Include(t => t.SubjectCodeNavigation)
                .FirstOrDefaultAsync(m => m.TopicsCode == id);
            if (topics == null)
            {
                return NotFound();
            }

            return View(topics);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var topics = await _context.Topics.FindAsync(id);
            _context.Topics.Remove(topics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicsExists(string id)
        {
            return _context.Topics.Any(e => e.TopicsCode == id);
        }
    }
}
