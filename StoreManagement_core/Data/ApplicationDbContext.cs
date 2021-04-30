using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using StoreManagement_core.Models;
using StoreManagement_core.ViewModels;

namespace StoreManagement_core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StoreManagement_core.Models.Category> Category { get; set; }
        public DbSet<StoreManagement_core.Models.Product> Product { get; set; }
        public DbSet<StoreManagement_core.Models.Stock> Stock { get; set; }
        public DbSet<StoreManagement_core.Models.Purchase> Purchase { get; set; }
        public DbSet<StoreManagement_core.Models.PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<StoreManagement_core.Models.Membership> Membership { get; set; }
        public DbSet<StoreManagement_core.Models.Customer> Customer { get; set; }
        public DbSet<StoreManagement_core.Models.Sales> Sales { get; set; }
        public DbSet<StoreManagement_core.Models.SalesDetail> SalesDetail { get; set; }
        public DbSet<StoreManagement_core.ViewModels.RemoveOldStockViewModel> RemoveOldStockViewModel { get; set; }

    }
}
