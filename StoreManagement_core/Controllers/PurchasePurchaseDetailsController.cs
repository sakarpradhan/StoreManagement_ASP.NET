using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_core.Data;
using StoreManagement_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Controllers
{
    public class PurchasePurchaseDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchasePurchaseDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchasePurchaseDetailsController/Create
        public ActionResult Create()
        {
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName");
            return View();
        }

        // POST: PurchasePurchaseDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Purchase NewPurchase = new Purchase();
                    NewPurchase.VendorName = collection["VendorName"];
                    NewPurchase.PurDate = DateTime.Parse(collection["PurDate"]);
                    _context.Purchase.Add(NewPurchase);
                    await _context.SaveChangesAsync();

                    for (int i = 0; i < collection["ProdId"].Count - 1; i++)
                    {
                        var stock = await _context.Stock.Where(x => x.ProdId == int.Parse(collection["ProdId"][i])).FirstOrDefaultAsync();

                        PurchaseDetail NewPurchaseDetail = new PurchaseDetail();
                        NewPurchaseDetail.ProdId = int.Parse(collection["ProdId"][i]);
                        NewPurchaseDetail.Price = int.Parse(collection["Price"][i]);
                        NewPurchaseDetail.Quantity = int.Parse(collection["Quantity"][i]);
                        NewPurchaseDetail.PurId = NewPurchase.PurId;

                        stock.StockedDate = DateTime.Now;
                        stock.Quantity += NewPurchaseDetail.Quantity;
                        _context.Stock.Update(stock);

                        _context.PurchaseDetail.Add(NewPurchaseDetail);
                        await _context.SaveChangesAsync();
                    }

                }
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
