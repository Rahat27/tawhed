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
    public class QuestionsController : Controller
    {
        private readonly ExamContext _context;

        public QuestionsController(ExamContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var examContext = _context.Questions.Include(q => q.TopicsCodeNavigation);
            return View(await examContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.TopicsCodeNavigation)
                .FirstOrDefaultAsync(m => m.QuestionNo == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["TopicsCode"] = new SelectList(_context.Topics, "TopicsCode", "TopicsCode");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionNo,Question,TopicsCode")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicsCode"] = new SelectList(_context.Topics, "TopicsCode", "TopicsCode", questions.TopicsCode);
            return View(questions);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            ViewData["TopicsCode"] = new SelectList(_context.Topics, "TopicsCode", "TopicsCode", questions.TopicsCode);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("QuestionNo,Question,TopicsCode")] Questions questions)
        {
            if (id != questions.QuestionNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.QuestionNo))
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
            ViewData["TopicsCode"] = new SelectList(_context.Topics, "TopicsCode", "TopicsCode", questions.TopicsCode);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.TopicsCodeNavigation)
                .FirstOrDefaultAsync(m => m.QuestionNo == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var questions = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(string id)
        {
            return _context.Questions.Any(e => e.QuestionNo == id);
        }
    }
}
