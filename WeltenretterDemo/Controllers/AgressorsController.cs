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
    public class AgressorsController : Controller
    {
        private readonly WeltenretterDemoContext _context;

        public AgressorsController(WeltenretterDemoContext context)
        {
            _context = context;
        }

        // GET: Agressors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agressor.ToListAsync());
        }

        // GET: Agressors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agressor = await _context.Agressor
                .SingleOrDefaultAsync(m => m.AgressorId == id);
            if (agressor == null)
            {
                return NotFound();
            }

            return View(agressor);
        }

        // GET: Agressors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agressors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgressorId,Agressorname")] Agressor agressor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agressor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agressor);
        }

        // GET: Agressors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agressor = await _context.Agressor.SingleOrDefaultAsync(m => m.AgressorId == id);
            if (agressor == null)
            {
                return NotFound();
            }
            return View(agressor);
        }

        // POST: Agressors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgressorId,Agressorname")] Agressor agressor)
        {
            if (id != agressor.AgressorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agressor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgressorExists(agressor.AgressorId))
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
            return View(agressor);
        }

        // GET: Agressors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agressor = await _context.Agressor
                .SingleOrDefaultAsync(m => m.AgressorId == id);
            if (agressor == null)
            {
                return NotFound();
            }

            return View(agressor);
        }

        // POST: Agressors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agressor = await _context.Agressor.SingleOrDefaultAsync(m => m.AgressorId == id);
            _context.Agressor.Remove(agressor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgressorExists(int id)
        {
            return _context.Agressor.Any(e => e.AgressorId == id);
        }
    }
}
