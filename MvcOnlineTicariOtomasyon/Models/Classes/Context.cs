using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Context:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> ınvoiceItems { get; set; }
      
        public DbSet<Category> Categories { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<SalesMovement> SalesMovements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CargoDetail> cargoDetails { get; set; }
        public DbSet<CargoTracking> cargoTrackings { get; set; }
      






    }
}