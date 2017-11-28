using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductionOfDetails;
using Microsoft.AspNetCore.Authorization;

namespace ProductionOfDetails.Controllers
{
    [Authorize]
    public class DetailsController : Controller
    {
        private readonly Production_of_detalsContext _context;

        public DetailsController(Production_of_detalsContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index()
        {
            var production_of_detalsContext = _context.Details.Include(d => d.IdMaterialNavigation).Include(d => d.IdOrderNavigation);
            return View(await production_of_detalsContext.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .Include(d => d.IdMaterialNavigation)
                .Include(d => d.IdOrderNavigation)
                .SingleOrDefaultAsync(m => m.IdDetail == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial");
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder");
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetail,IdMaterial,Weight,Colour,Diameter,Cost,IdOrder,AmountDetails")] Details details)
        {
            if (ModelState.IsValid)
            {
                _context.Add(details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial", details.IdMaterial);
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", details.IdOrder);
            return View(details);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details.SingleOrDefaultAsync(m => m.IdDetail == id);
            if (details == null)
            {
                return NotFound();
            }
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial", details.IdMaterial);
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", details.IdOrder);
            return View(details);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetail,IdMaterial,Weight,Colour,Diameter,Cost,IdOrder,AmountDetails")] Details details)
        {
            if (id != details.IdDetail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsExists(details.IdDetail))
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
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial", details.IdMaterial);
            ViewData["IdOrder"] = new SelectList(_context.Orders, "IdOrder", "IdOrder", details.IdOrder);
            return View(details);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .Include(d => d.IdMaterialNavigation)
                .Include(d => d.IdOrderNavigation)
                .SingleOrDefaultAsync(m => m.IdDetail == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var details = await _context.Details.SingleOrDefaultAsync(m => m.IdDetail == id);
            _context.Details.Remove(details);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailsExists(int id)
        {
            return _context.Details.Any(e => e.IdDetail == id);
        }
    }
}
