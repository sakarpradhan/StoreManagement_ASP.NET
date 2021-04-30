using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement_core.Data;
using StoreManagement_core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Controllers
{
    public class RemoveOldStocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RemoveOldStocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RemoveOldStocksController
        public ActionResult Index()
        {

            List<RemoveOldStockViewModel> itemStockData = new List<RemoveOldStockViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT p.ProdId as ProductID,p.ProdName as ProductName,ps.Quantity,ps.StockedDate from Product p inner join Stock ps on p.ProdId=ps.ProdId where ps.StockedDate <= DATEADD(DAY, -183, GETDATE()) ";
                _context.Database.OpenConnection();
                

                using (var result = command.ExecuteReader())
                {
                    RemoveOldStockViewModel data;
                    while (result.Read())
                    {
                        data = new RemoveOldStockViewModel();
                        data.ProdId = result.GetInt32(0);
                        data.ProdName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        data.StockedDate = result.GetDateTime(3);
                        itemStockData.Add(data);
                    }
                }
            }
            return View(itemStockData);


        }
    
    
   
           
        

        // GET: RemoveOldStocksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RemoveOldStocksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var product = await _context.Product.Where(x => x.ProdId == id).FirstOrDefaultAsync();
                var stock = await _context.Stock.Where(x => x.ProdId == id).FirstOrDefaultAsync();
                if (product != null && stock != null)
                {
                    _context.Product.Remove(product);
                    _context.Stock.Remove(stock);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
