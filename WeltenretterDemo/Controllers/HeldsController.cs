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
    public class HeldsController : Controller
    {
        private readonly WeltenretterDemoContext _context;

        public HeldsController(WeltenretterDemoContext context)
        {
            _context = context;
        }

        // GET: Helds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Held.ToListAsync());
        }

        // GET: Helds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var held = await _context.Held
                .SingleOrDefaultAsync(m => m.HeldId == id);
            if (held == null)
            {
                return NotFound();
            }

            return View(held);
        }

        // GET: Helds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Helds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HeldId,Heldname")] Held held)
        {
            if (ModelState.IsValid)
            {
                _context.Add(held);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(held);
        }

        // GET: Helds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var held = await _context.Held.SingleOrDefaultAsync(m => m.HeldId == id);
            if (held == null)
            {
                return NotFound();
            }
            return View(held);
        }

        // POST: Helds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HeldId,Heldname")] Held held)
        {
            if (id != held.HeldId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(held);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeldExists(held.HeldId))
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
            return View(held);
        }

        // GET: Helds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var held = await _context.Held
                .SingleOrDefaultAsync(m => m.HeldId == id);
            if (held == null)
            {
                return NotFound();
            }

            return View(held);
        }

        // POST: Helds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var held = await _context.Held.SingleOrDefaultAsync(m => m.HeldId == id);
            _context.Held.Remove(held);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeldExists(int id)
        {
            return _context.Held.Any(e => e.HeldId == id);
        }
    }
}
