using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationInCore.Models.DB;

namespace StudentRegistrationInCore.Controllers
{
    public class TblStudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public TblStudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: TblStudents
        public async Task<IActionResult> Index()
        {
            var studentDbContext = _context.TblStudents.Include(t => t.Class).Include(t => t.Doc).Include(t => t.Gender).Include(t => t.Image);
            return View(await studentDbContext.ToListAsync());
        }

        // GET: TblStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblStudents == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents
                .Include(t => t.Class)
                .Include(t => t.Doc)
                .Include(t => t.Gender)
                .Include(t => t.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // GET: TblStudents/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.TblClasses, "ClassId", "ClassId");
            ViewData["DocId"] = new SelectList(_context.TblDocuments, "DocId", "DocId");
            ViewData["GenderId"] = new SelectList(_context.TblGenders, "GenderId", "GenderId");
            ViewData["ImageId"] = new SelectList(_context.TblImages, "ImageId", "ImageId");
            return View();
        }

        // POST: TblStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,ClassId,Phone,DocId,RegisteredDate,GenderId,ImageId,Hobbies,Football,Basketball,Cricket,Chess,Tennis,Drawing")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.TblClasses, "ClassId", "ClassId", tblStudent.ClassId);
            ViewData["DocId"] = new SelectList(_context.TblDocuments, "DocId", "DocId", tblStudent.DocId);
            ViewData["GenderId"] = new SelectList(_context.TblGenders, "GenderId", "GenderId", tblStudent.GenderId);
            ViewData["ImageId"] = new SelectList(_context.TblImages, "ImageId", "ImageId", tblStudent.ImageId);
            return View(tblStudent);
        }

        // GET: TblStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblStudents == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents.FindAsync(id);
            if (tblStudent == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.TblClasses, "ClassId", "ClassId", tblStudent.ClassId);
            ViewData["DocId"] = new SelectList(_context.TblDocuments, "DocId", "DocId", tblStudent.DocId);
            ViewData["GenderId"] = new SelectList(_context.TblGenders, "GenderId", "GenderId", tblStudent.GenderId);
            ViewData["ImageId"] = new SelectList(_context.TblImages, "ImageId", "ImageId", tblStudent.ImageId);
            return View(tblStudent);
        }

        // POST: TblStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,ClassId,Phone,DocId,RegisteredDate,GenderId,ImageId,Hobbies,Football,Basketball,Cricket,Chess,Tennis,Drawing")] TblStudent tblStudent)
        {
            if (id != tblStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblStudentExists(tblStudent.Id))
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
            ViewData["ClassId"] = new SelectList(_context.TblClasses, "ClassId", "ClassId", tblStudent.ClassId);
            ViewData["DocId"] = new SelectList(_context.TblDocuments, "DocId", "DocId", tblStudent.DocId);
            ViewData["GenderId"] = new SelectList(_context.TblGenders, "GenderId", "GenderId", tblStudent.GenderId);
            ViewData["ImageId"] = new SelectList(_context.TblImages, "ImageId", "ImageId", tblStudent.ImageId);
            return View(tblStudent);
        }

        // GET: TblStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblStudents == null)
            {
                return NotFound();
            }

            var tblStudent = await _context.TblStudents
                .Include(t => t.Class)
                .Include(t => t.Doc)
                .Include(t => t.Gender)
                .Include(t => t.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblStudent == null)
            {
                return NotFound();
            }

            return View(tblStudent);
        }

        // POST: TblStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblStudents == null)
            {
                return Problem("Entity set 'StudentDbContext.TblStudents'  is null.");
            }
            var tblStudent = await _context.TblStudents.FindAsync(id);
            if (tblStudent != null)
            {
                _context.TblStudents.Remove(tblStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblStudentExists(int id)
        {
          return (_context.TblStudents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
