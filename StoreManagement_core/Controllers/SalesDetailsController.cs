using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_core.Data;
using StoreManagement_core.Models;

namespace StoreManagement_core.Controllers
{
    public class SalesDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalesDetail.Include(s => s.Prod).Include(s => s.Sale);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalesDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetail = await _context.SalesDetail
                .Include(s => s.Prod)
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SalesDetailId == id);
            if (salesDetail == null)
            {
                return NotFound();
            }

            return View(salesDetail);
        }

        // GET: SalesDetails/Create
        public IActionResult Create()
        {
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName");
            ViewData["SalesId"] = new SelectList(_context.Sales, "SalesId", "BillNo");
            return View();
        }

        // POST: SalesDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesDetailId,SalesId,ProdId,Quantity,Amount")] SalesDetail salesDetail)
        {
            if (ModelState.IsValid)
            {
                var stock = await _context.Stock.Where(x => x.ProdId == salesDetail.ProdId).FirstOrDefaultAsync();
                if ( stock.Quantity < salesDetail.Quantity )
                {
                    String error = "Stock available for this Product is " + stock.Quantity + " units.";
                    ModelState.AddModelError("Quantity", error);
                    return View(salesDetail);
                } else
                {
                    stock.Quantity -= salesDetail.Quantity;
                    _context.Update(stock);
                }
                
                _context.Add(salesDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName", salesDetail.ProdId);
            ViewData["SalesId"] = new SelectList(_context.Sales, "SalesId", "BillNo", salesDetail.SalesId);
            return View(salesDetail);
        }

        // GET: SalesDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetail = await _context.SalesDetail.FindAsync(id);
            if (salesDetail == null)
            {
                return NotFound();
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName", salesDetail.ProdId);
            ViewData["SalesId"] = new SelectList(_context.Sales, "SalesId", "BillNo", salesDetail.SalesId);
            return View(salesDetail);
        }

        // POST: SalesDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesDetailId,SalesId,ProdId,Quantity,Amount")] SalesDetail salesDetail)
        {
            if (id != salesDetail.SalesDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesDetailExists(salesDetail.SalesDetailId))
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
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName", salesDetail.ProdId);
            ViewData["SalesId"] = new SelectList(_context.Sales, "SalesId", "BillNo", salesDetail.SalesId);
            return View(salesDetail);
        }

        // GET: SalesDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetail = await _context.SalesDetail
                .Include(s => s.Prod)
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SalesDetailId == id);
            if (salesDetail == null)
            {
                return NotFound();
            }

            return View(salesDetail);
        }

        // POST: SalesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesDetail = await _context.SalesDetail.FindAsync(id);
            _context.SalesDetail.Remove(salesDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesDetailExists(int id)
        {
            return _context.SalesDetail.Any(e => e.SalesDetailId == id);
        }
    }
}
