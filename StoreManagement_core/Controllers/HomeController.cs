using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreManagement_core.Data;
using StoreManagement_core.Models;
using StoreManagement_core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement_core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            int count = 0;
            List<StockAlertViewModel> lstData = new List<StockAlertViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT p.ProdId as ProductID,p.ProdName as ProductName,ps.Quantity from Product p inner join Stock ps on p.ProdId=ps.ProdId where Quantity < 10";
                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    StockAlertViewModel data;
                    while (result.Read())
                    {
                        count = count + 1;
                        data = new StockAlertViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }
            if (count > 0)
            {
                return View(lstData);
            }
            else
            {
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
