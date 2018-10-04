using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineExamManagement.Models.Db;

namespace OnlineExamManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ExamContext _context;

        public StudentsController(ExamContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,Class,Father,Picture")] Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,Class,Father,Picture")] Students students)
        {
            if (id != students.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.StudentId))
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
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }


        // Active Picture
        // Starts
        [HttpPost]
        public string ActivatePicture(int id, string path)
        {
            string a = "1";
            Students st = (from s in _context.Students where s.StudentId == id select s).First();
            st.Picture = path;
            _context.SaveChanges();


            return a;

        }
        // Ends



        public ActionResult UploadFile(int id)
        {

            // To show all pictures in upload page

            // Starts
            List<Itempictures> content = (from p in _context.Itempictures
                                          where p.StudentId == id
                                          orderby p.Slno
                                          select p).ToList();
            ViewData["pictures"] = content;
            string path = (from s in _context.Students where s.StudentId == id select s.Picture).FirstOrDefault();
            ViewData["apath"] = path;
            // Ends


            return View(_context.Students.Find(id));
        }

        [HttpPost, ActionName("UploadFile")]
        public ActionResult UploadFile(IFormFile file, int id)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
                if (file.Length > 0)
                {



                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/" + file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Students st = (from s in _context.Students where s.StudentId == id select s).First();
                    st.Picture = file.FileName;
                    _context.SaveChanges();

                    // For activating one picture from multiple pictues

                    // Starts
                    int psl = (from ip in _context.Itempictures where ip.StudentId == id orderby ip.Slno descending select ip.Slno).FirstOrDefault();
                    psl++;
                    Itempictures ipc = new Itempictures();
                    ipc.StudentId = id;
                    ipc.Slno = psl;
                    ipc.Picturepath = file.FileName;
                    _context.Add(ipc);
                    _context.SaveChanges();
                    // Ends

                }
                ViewBag.Message = "File Uploaded Successfully!!" + ":" + id;

                return RedirectToAction("Index");

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }


        }
    

    }
}

