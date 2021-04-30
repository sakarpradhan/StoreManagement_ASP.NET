using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_core.Data;
using StoreManagement_core.Models;
using StoreManagement_core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Controllers
{
    public class SalesSalesDetails : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesSalesDetails(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustName");
            ViewData["ProdId"] = new SelectList(_context.Product, "ProdId", "ProdName");
            return View();
        }

        // POST: SalesSalesDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // First Loop check the quantity for each product vs available quantity in stock
                    for (int i = 0; i < collection["Quantity"].Count - 1; i++)
                    {
                        System.Diagnostics.Debug.WriteLine("value of i: " + i);

                        var stock = await _context.Stock.Where(x => x.ProdId == int.Parse(collection["ProdId"][i])).FirstOrDefaultAsync();
                        if (stock.Quantity < int.Parse(collection["Quantity"][i]))
                        {
                            String error = "Stock available for this Product is " + stock.Quantity + " units.";
                            ModelState.AddModelError("Quantity", error);
                            return View();
                        }
                    }

                    Sales NewSales = new Sales();
                    NewSales.CustId = int.Parse(collection["CustId"]);
                    NewSales.BillNo = collection["BillNo"];
                    NewSales.SalesDate = DateTime.Parse(collection["SalesDate"]);
                    NewSales.Remarks = collection["Remarks"];
                    _context.Sales.Add(NewSales);
                    await _context.SaveChangesAsync();

                    // Second Loop Make Purchase and commit
                    for (int i = 0; i < collection["Quantity"].Count - 1; i++)
                    {
                        System.Diagnostics.Debug.WriteLine("......2nd loop iteration => " + i);

                        var stock = await _context.Stock.Where(x => x.ProdId == int.Parse(collection["ProdId"][i])).FirstOrDefaultAsync();

                        SalesDetail NewSalesDetail = new SalesDetail();
                        NewSalesDetail.ProdId = int.Parse(collection["ProdId"][i]);
                        NewSalesDetail.Quantity = int.Parse(collection["Quantity"][i]);
                        NewSalesDetail.Amount = int.Parse(collection["Amount"][i]);
                        NewSalesDetail.SalesId = NewSales.SalesId;

                        System.Diagnostics.Debug.WriteLine("The sales id is: " + NewSalesDetail.SalesId);

                        stock.Quantity -= NewSalesDetail.Quantity;

                        _context.Add(NewSalesDetail);
                        _context.Update(stock);

                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Create));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
