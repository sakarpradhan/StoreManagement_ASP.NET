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
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*
         * Precursor to Question no. 5
        */
            public IActionResult StockListReport()
        {
            List<ProductStockViewModel> lstData = new List<ProductStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT p.ProdId, p.ProdName, ps.Quantity FROM Product p INNER JOIN Stock ps ON p.ProdId = ps.ProdId";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    
                    ProductStockViewModel data;

                    while (result.Read())
                    {
                        data = new ProductStockViewModel();
                        data.ProdId = result.GetInt32(0);
                        data.ProdName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        /*
         * Question No. 5 Solution
        */
        public IActionResult StockListSearchReport(String SearchBy)
        {
            List<String> ProductNames = new List<string>();
            List<ProductStockViewModel> lstData = new List<ProductStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT p.ProdId, p.ProdName, ps.Quantity FROM Product p INNER JOIN Stock ps ON p.ProdId = ps.ProdId";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    ProductStockViewModel data;
                    while (result.Read())
                    {
                        data = new ProductStockViewModel();
                        data.ProdId = result.GetInt32(0);
                        data.ProdName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                        ProductNames.Add(data.ProdName.ToString());
                    }
                }
            }
            ViewData["ProductNames"] = ProductNames;
            return View( lstData.Where( x => SearchBy != null && x.ProdName.ToLower().Contains(SearchBy.ToLower()) ) );
        }

        /*
         * Question No. 6 Solution
        */
        public IActionResult StockListItemReport(String SearchBy)
        {
            List<ProductStockViewModel> lstData = new List<ProductStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT p.ProdId, p.ProdName, ps.Quantity FROM Product p INNER JOIN Stock ps ON p.ProdId = ps.ProdId";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {

                    ProductStockViewModel data;

                    while (result.Read())
                    {
                        data = new ProductStockViewModel();
                        data.ProdId = result.GetInt32(0);
                        data.ProdName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData.Where(x => x.ProdName == SearchBy));
        }

        /*
         * Question No. 8 Solution
         * Purchase History of Customer
        */
        public IActionResult CustomerPurchaseHistoryReport(String SearchBy)
        {
            List<String> CustomerNames = new List<string>();
            List<CustomerPurchaseHistoryViewModel> lstData = new List<CustomerPurchaseHistoryViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT c.CustName, p.ProdName, sd.Quantity, sd.Amount, s.SalesDate, s.BillNo FROM Customer c JOIN Sales s ON(c.CustId = s.CustId) JOIN SalesDetail sd ON(s.SalesId = sd.SalesId) JOIN Product p ON(sd.ProdId = p.ProdId) WHERE s.SalesDate >= DATEADD(DAY, -31, GETDATE()) AND s.SalesDate <= GETDATE()";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {

                    CustomerPurchaseHistoryViewModel data;

                    while (result.Read())
                    {
                        data = new CustomerPurchaseHistoryViewModel();
                        data.CustName = result.GetString(0);
                        data.ProdName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        data.Amount = result.GetDouble(3);
                        data.SalesDate = result.GetDateTime(4);
                        data.BillNo = result.GetString(5);
                        CustomerNames.Add(data.CustName.ToString());
                        lstData.Add(data);
                    }
                }
            }
            ViewData["CustomerNames"] = CustomerNames;
            return View(lstData.Where(x => SearchBy != null && x.CustName.ToLower().Contains(SearchBy.ToLower())));
        }

        /*
         * Question No. 12 Solution
         * Purchase History of Customer
        */
        public IActionResult CustomerLastMonthReport ()
        {
            List<CustomersLastMonthViewModel> lstData = new List<CustomersLastMonthViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT c.CustName, s.SalesDate FROM Customer c JOIN Sales s ON(c.CustId = s.CustId) WHERE s.SalesDate < DATEADD(DAY, -31, GETDATE())";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {

                    CustomersLastMonthViewModel data;

                    while (result.Read())
                    {
                        data = new CustomersLastMonthViewModel();
                        data.CustName = result.GetString(0);
                        data.SalesDate = result.GetDateTime(1);
                        
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        /*
         * Question No. 13 Solution
         * Purchase History of Customer
        */
        public IActionResult UnsoldStockReport()
        {
            List<UnsoldStockViewModel> lstData = new List<UnsoldStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT ProdName FROM Product WHERE NOT EXISTS (SELECT p.ProdName FROM Product p JOIN SalesDetail sd ON (sd.ProdId = p.ProdId) JOIN Sales s ON (sd.SalesId = s.SalesId) WHERE s.SalesDate > DATEADD(DAY, -31, GETDATE()))";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {

                    UnsoldStockViewModel data;

                    while (result.Read())
                    {
                        data = new UnsoldStockViewModel();
                        data.ProdName = result.GetString(0);
                        //data.SalesDate = result.GetDateTime(1);

                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }


        /*
         * Question No. 11 Solution
         * Items out of stock
        */
        public IActionResult ItemOutOfStockReport(String sortData)
        {
            string sortOrder = sortData;
            List<ItemOutOfStockViewModel> itemStockData = new List<ItemOutOfStockViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT p.ProdId as ProductID,p.ProdName as ProductName,ps.Quantity,ps.StockedDate from Product p inner join Stock ps on p.ProdId=ps.ProdId where ps.Quantity < 10";
                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    ItemOutOfStockViewModel data;
                    while (result.Read())
                    {
                        data = new ItemOutOfStockViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        data.StockedDate = result.GetDateTime(3);
                        itemStockData.Add(data);
                    }
                }
            }
            switch (sortOrder)
            {
                case "ProductName":
                    return View(itemStockData.OrderBy(x => x.ProductName));
                    break;
                case "hightolow":
                    return View(itemStockData.OrderByDescending(x => x.Quantity));
                    break;
                case "date":
                    return View(itemStockData.OrderByDescending(x => x.StockedDate));
                    break;
                default:
                    return View(itemStockData);
                    break;
            }
        }
    }
}
