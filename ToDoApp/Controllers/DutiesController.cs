using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class DutiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DutiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Duties != null ?
                        View(await _context.Duties.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Duties'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Duties == null)
            {
                return NotFound();
            }

            var duty = await _context.Duties
                .FirstOrDefaultAsync(m => m.ID == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Detail,DeadLine")] Duty duty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duty);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Duties == null)
            {
                return NotFound();
            }

            var duty = await _context.Duties.FindAsync(id);
            if (duty == null)
            {
                return NotFound();
            }
            return View(duty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Detail,DeadLine")] Duty duty)
        {
            if (id != duty.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyExists(duty.ID))
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
            return View(duty);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Duties == null)
            {
                return NotFound();
            }

            var duty = await _context.Duties
                .FirstOrDefaultAsync(m => m.ID == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Duties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Duties'  is null.");
            }
            var duty = await _context.Duties.FindAsync(id);
            if (duty != null)
            {
                _context.Duties.Remove(duty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DutyExists(int id)
        {
            return (_context.Duties?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
