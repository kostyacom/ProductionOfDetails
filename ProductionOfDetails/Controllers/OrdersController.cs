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
    public class OrdersController : Controller
    {
        private readonly Production_of_detalsContext _context;

        public OrdersController(Production_of_detalsContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var production_of_detalsContext = _context.Orders.Include(o => o.IdClientNavigation).Include(o => o.IdEmployeeNavigation);
            return View(await production_of_detalsContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.IdClientNavigation)
                .Include(o => o.IdEmployeeNavigation)
                .SingleOrDefaultAsync(m => m.IdOrder == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient");
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrder,IdClient,IdEmployee,DetailsAmount,OrderCost,Discount,AmountOrdersOnDate,Chek")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", orders.IdClient);
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", orders.IdEmployee);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.SingleOrDefaultAsync(m => m.IdOrder == id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", orders.IdClient);
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", orders.IdEmployee);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrder,IdClient,IdEmployee,DetailsAmount,OrderCost,Discount,AmountOrdersOnDate,Chek")] Orders orders)
        {
            if (id != orders.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.IdOrder))
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
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", orders.IdClient);
            ViewData["IdEmployee"] = new SelectList(_context.Employees, "IdEmployee", "IdEmployee", orders.IdEmployee);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.IdClientNavigation)
                .Include(o => o.IdEmployeeNavigation)
                .SingleOrDefaultAsync(m => m.IdOrder == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.SingleOrDefaultAsync(m => m.IdOrder == id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.IdOrder == id);
        }
    }
}
