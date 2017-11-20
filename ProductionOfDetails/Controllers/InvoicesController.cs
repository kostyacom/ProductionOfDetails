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
    public class InvoicesController : Controller
    {
        private readonly Production_of_detalsContext _context;

        public InvoicesController(Production_of_detalsContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var production_of_detalsContext = _context.Invoices.Include(i => i.IdEmployeeNavigation).Include(i => i.IdMaterialNavigation).Include(i => i.IdSupplierNavigation);
            return View(await production_of_detalsContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices
                .Include(i => i.IdEmployeeNavigation)
                .Include(i => i.IdMaterialNavigation)
                .Include(i => i.IdSupplierNavigation)
                .SingleOrDefaultAsync(m => m.IdInvoice == id);
            if (invoices == null)
            {
                return NotFound();
            }

            return View(invoices);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee");
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial");
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "IdSupplier");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvoice,IdSupplier,IdEmployee,IdMaterial,DeliveryDate,Cost,Weight")] Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", invoices.IdEmployee);
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial", invoices.IdMaterial);
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "IdSupplier", invoices.IdSupplier);
            return View(invoices);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices.SingleOrDefaultAsync(m => m.IdInvoice == id);
            if (invoices == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", invoices.IdEmployee);
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial", invoices.IdMaterial);
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "IdSupplier", invoices.IdSupplier);
            return View(invoices);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInvoice,IdSupplier,IdEmployee,IdMaterial,DeliveryDate,Cost,Weight")] Invoices invoices)
        {
            if (id != invoices.IdInvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoicesExists(invoices.IdInvoice))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", invoices.IdEmployee);
            ViewData["IdMaterial"] = new SelectList(_context.MaterialTypes, "IdMaterial", "IdMaterial", invoices.IdMaterial);
            ViewData["IdSupplier"] = new SelectList(_context.Suppliers, "IdSupplier", "IdSupplier", invoices.IdSupplier);
            return View(invoices);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices
                .Include(i => i.IdEmployeeNavigation)
                .Include(i => i.IdMaterialNavigation)
                .Include(i => i.IdSupplierNavigation)
                .SingleOrDefaultAsync(m => m.IdInvoice == id);
            if (invoices == null)
            {
                return NotFound();
            }

            return View(invoices);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoices = await _context.Invoices.SingleOrDefaultAsync(m => m.IdInvoice == id);
            _context.Invoices.Remove(invoices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoicesExists(int id)
        {
            return _context.Invoices.Any(e => e.IdInvoice == id);
        }
    }
}
