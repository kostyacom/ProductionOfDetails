using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductionOfDetails;

namespace ProductionOfDetails.Controllers
{
    public class MaterialTypesController : Controller
    {
        private readonly Production_of_detalsContext _context;

        public MaterialTypesController(Production_of_detalsContext context)
        {
            _context = context;
        }

        // GET: MaterialTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaterialTypes.ToListAsync());
        }

        // GET: MaterialTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialTypes = await _context.MaterialTypes
                .SingleOrDefaultAsync(m => m.IdMaterial == id);
            if (materialTypes == null)
            {
                return NotFound();
            }

            return View(materialTypes);
        }

        // GET: MaterialTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMaterial,TypeMaterial")] MaterialTypes materialTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materialTypes);
        }

        // GET: MaterialTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialTypes = await _context.MaterialTypes.SingleOrDefaultAsync(m => m.IdMaterial == id);
            if (materialTypes == null)
            {
                return NotFound();
            }
            return View(materialTypes);
        }

        // POST: MaterialTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMaterial,TypeMaterial")] MaterialTypes materialTypes)
        {
            if (id != materialTypes.IdMaterial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialTypesExists(materialTypes.IdMaterial))
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
            return View(materialTypes);
        }

        // GET: MaterialTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialTypes = await _context.MaterialTypes
                .SingleOrDefaultAsync(m => m.IdMaterial == id);
            if (materialTypes == null)
            {
                return NotFound();
            }

            return View(materialTypes);
        }

        // POST: MaterialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialTypes = await _context.MaterialTypes.SingleOrDefaultAsync(m => m.IdMaterial == id);
            _context.MaterialTypes.Remove(materialTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialTypesExists(int id)
        {
            return _context.MaterialTypes.Any(e => e.IdMaterial == id);
        }
    }
}
