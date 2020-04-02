using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeltenretterDemo.Models;

namespace WeltenretterDemo.Controllers
{
    public class HeldAgressorsController : Controller
    {
        private readonly WeltenretterDemoContext _context;

        public HeldAgressorsController(WeltenretterDemoContext context)
        {
            _context = context;
        }

        // GET: HeldAgressors
        public async Task<IActionResult> Index()
        {
            var weltenretterDemoContext = _context.HeldAgressor.Include(h => h.Agressor).Include(h => h.Held);
            return View(await weltenretterDemoContext.ToListAsync());
        }

        // GET: HeldAgressors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heldAgressor = await _context.HeldAgressor
                .Include(h => h.Agressor)
                .Include(h => h.Held)
                .SingleOrDefaultAsync(m => m.HeldagressorId == id);
            if (heldAgressor == null)
            {
                return NotFound();
            }

            return View(heldAgressor);
        }

        // GET: HeldAgressors/Create
        public IActionResult Create()
        {
            ViewData["AgressorId"] = new SelectList(_context.Agressor, "AgressorId", "AgressorId");
            ViewData["HeldId"] = new SelectList(_context.Held, "HeldId", "HeldId");
            return View();
        }

        // POST: HeldAgressors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeldagressorId,HeldId,AgressorId")] HeldAgressor heldAgressor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heldAgressor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgressorId"] = new SelectList(_context.Agressor, "AgressorId", "AgressorId", heldAgressor.AgressorId);
            ViewData["HeldId"] = new SelectList(_context.Held, "HeldId", "HeldId", heldAgressor.HeldId);
            return View(heldAgressor);
        }

        // GET: HeldAgressors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heldAgressor = await _context.HeldAgressor.SingleOrDefaultAsync(m => m.HeldagressorId == id);
            if (heldAgressor == null)
            {
                return NotFound();
            }
            ViewData["AgressorId"] = new SelectList(_context.Agressor, "AgressorId", "AgressorId", heldAgressor.AgressorId);
            ViewData["HeldId"] = new SelectList(_context.Held, "HeldId", "HeldId", heldAgressor.HeldId);
            return View(heldAgressor);
        }

        // POST: HeldAgressors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeldagressorId,HeldId,AgressorId")] HeldAgressor heldAgressor)
        {
            if (id != heldAgressor.HeldagressorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heldAgressor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeldAgressorExists(heldAgressor.HeldagressorId))
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
            ViewData["AgressorId"] = new SelectList(_context.Agressor, "AgressorId", "AgressorId", heldAgressor.AgressorId);
            ViewData["HeldId"] = new SelectList(_context.Held, "HeldId", "HeldId", heldAgressor.HeldId);
            return View(heldAgressor);
        }

        // GET: HeldAgressors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heldAgressor = await _context.HeldAgressor
                .Include(h => h.Agressor)
                .Include(h => h.Held)
                .SingleOrDefaultAsync(m => m.HeldagressorId == id);
            if (heldAgressor == null)
            {
                return NotFound();
            }

            return View(heldAgressor);
        }

        // POST: HeldAgressors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var heldAgressor = await _context.HeldAgressor.SingleOrDefaultAsync(m => m.HeldagressorId == id);
            _context.HeldAgressor.Remove(heldAgressor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeldAgressorExists(int id)
        {
            return _context.HeldAgressor.Any(e => e.HeldagressorId == id);
        }
    }
}
