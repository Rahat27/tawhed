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
    public class AnswersController : Controller
    {
        private readonly ExamContext _context;

        public AnswersController(ExamContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var examContext = _context.Answers.Include(a => a.QuestionNoNavigation);
            return View(await examContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.QuestionNoNavigation)
                .FirstOrDefaultAsync(m => m.AnswerSerial == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["QuestionNo"] = new SelectList(_context.Questions, "QuestionNo", "QuestionNo");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerSerial,Answer,Correct,QuestionNo")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionNo"] = new SelectList(_context.Questions, "QuestionNo", "QuestionNo", answers.QuestionNo);
            return View(answers);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }
            ViewData["QuestionNo"] = new SelectList(_context.Questions, "QuestionNo", "QuestionNo", answers.QuestionNo);
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AnswerSerial,Answer,Correct,QuestionNo")] Answers answers)
        {
            if (id != answers.AnswerSerial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(answers.AnswerSerial))
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
            ViewData["QuestionNo"] = new SelectList(_context.Questions, "QuestionNo", "QuestionNo", answers.QuestionNo);
            return View(answers);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.QuestionNoNavigation)
                .FirstOrDefaultAsync(m => m.AnswerSerial == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var answers = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswersExists(string id)
        {
            return _context.Answers.Any(e => e.AnswerSerial == id);
        }
    }
}
