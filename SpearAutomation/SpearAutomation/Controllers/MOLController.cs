using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpearAutomation.Models.MOL;

namespace SpearAutomation.Controllers
{
    public class MOLController : Controller
    {
        private readonly SPEARMOLContext _context;

        public MOLController(SPEARMOLContext context)
        {
            _context = context;
        }

        // GET: MOL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personnel.ToListAsync());
        }

        // GET: MOL/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .FirstOrDefaultAsync(m => m.MarineId == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: MOL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MOL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarineId,DateReturning,Location")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.Personnel.FirstOrDefault(x => x.MarineId == personnel.MarineId);
                if(entity == null)
                {
                    _context.Add(personnel);
                } else
                {
                    entity.DateReturning = personnel.DateReturning;
                    entity.Location = personnel.Location;
                    _context.Update(entity);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: MOL/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }
            return View(personnel);
        }

        // POST: MOL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarineId,DateReturning,Location")] Personnel personnel)
        {
            if (id != personnel.MarineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.MarineId))
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
            return View(personnel);
        }

        // GET: MOL/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personnel = await _context.Personnel
                .FirstOrDefaultAsync(m => m.MarineId == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // POST: MOL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personnel = await _context.Personnel.FindAsync(id);
            _context.Personnel.Remove(personnel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelExists(int id)
        {
            return _context.Personnel.Any(e => e.MarineId == id);
        }
    }
}
