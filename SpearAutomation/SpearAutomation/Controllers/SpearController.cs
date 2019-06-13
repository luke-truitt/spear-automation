using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpearAutomation.Models.Logger.Data;
using SpearAutomation.Models.Logger.Model;
using SpearAutomation.Models.Spear.Models;

namespace SpearAutomation.Controllers
{
    public class SpearController : Controller
    {
        private readonly LoggerContext _context;

        public SpearController(LoggerContext context)
        {
            _context = context;
        }

        // GET: Spear
        public async Task<IActionResult> Index()
        {
            var dashboard = new DashboardModel();
            var logs = _context.EventLog.Where(x => x.EventId == (int) (LoggingEvent.UPDATE_ITEM) || x.EventId == (int)(LoggingEvent.CREATE_ITEM)).ToList();
            dashboard.logs = logs;
            return View(dashboard);
        }

        // GET: Spear/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventLog == null)
            {
                return NotFound();
            }

            return View(eventLog);
        }

        // GET: Spear/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spear/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,LogLevel,Message,CreatedTime")] EventLog eventLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventLog);
        }

        // GET: Spear/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLog.FindAsync(id);
            if (eventLog == null)
            {
                return NotFound();
            }
            return View(eventLog);
        }

        // POST: Spear/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,LogLevel,Message,CreatedTime")] EventLog eventLog)
        {
            if (id != eventLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventLogExists(eventLog.Id))
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
            return View(eventLog);
        }

        // GET: Spear/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventLog == null)
            {
                return NotFound();
            }

            return View(eventLog);
        }

        // POST: Spear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventLog = await _context.EventLog.FindAsync(id);
            _context.EventLog.Remove(eventLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventLogExists(int id)
        {
            return _context.EventLog.Any(e => e.Id == id);
        }

        public IActionResult ClearLog()
        {
            var logs = _context.EventLog;
            _context.RemoveRange(logs);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
