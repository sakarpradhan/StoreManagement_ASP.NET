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
    public class PurchaseDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseDetail.Include(p => p.Prod).Include(p => p.Pur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetail = await _context.PurchaseDetail
                .Include(p => p.Prod)
                .Include(p => p.Pur)
                .FirstOrDefaultAsync(m => m.PurDetId == id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }

            return View(purchaseDetail);
        }

        // GET: PurchaseDetails/Create
        public IActionResult Create()
        {
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName");
            ViewData["PurId"] = new SelectList(_context.Purchase, "PurId", "VendorName");
            return View();
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurDetId,PurId,ProdId,Price,Quantity")] PurchaseDetail purchaseDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseDetail);

                var stock = await _context.Stock.Where(x => x.ProdId == purchaseDetail.ProdId).FirstOrDefaultAsync();
                stock.Quantity += purchaseDetail.Quantity;
                
                _context.Update(stock);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdId", purchaseDetail.ProdId);
            ViewData["PurId"] = new SelectList(_context.Purchase, "PurId", "PurId", purchaseDetail.PurId);
            return View(purchaseDetail);
        }

        // GET: PurchaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetail = await _context.PurchaseDetail.FindAsync(id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName", purchaseDetail.ProdId);
            ViewData["PurId"] = new SelectList(_context.Purchase, "PurId", "VendorName", purchaseDetail.PurId);
            return View(purchaseDetail);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurDetId,PurId,ProdId,Price,Quantity")] PurchaseDetail purchaseDetail)
        {
            if (id != purchaseDetail.PurDetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailExists(purchaseDetail.PurDetId))
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
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName", purchaseDetail.ProdId);
            ViewData["PurId"] = new SelectList(_context.Purchase, "PurId", "VendorName", purchaseDetail.PurId);
            return View(purchaseDetail);
        }

        // GET: PurchaseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetail = await _context.PurchaseDetail
                .Include(p => p.Prod)
                .Include(p => p.Pur)
                .FirstOrDefaultAsync(m => m.PurDetId == id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }

            return View(purchaseDetail);
        }

        // POST: PurchaseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseDetail = await _context.PurchaseDetail.FindAsync(id);
            _context.PurchaseDetail.Remove(purchaseDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseDetailExists(int id)
        {
            return _context.PurchaseDetail.Any(e => e.PurDetId == id);
        }
    }
}
