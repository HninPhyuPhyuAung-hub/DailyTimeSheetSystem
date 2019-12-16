using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace myanmarkido.Models
{
    public class dailysheetcontext : DbContext
    {
        public dailysheetcontext() : base("name=myanmarkido")
        {
            Database.SetInitializer<dailysheetcontext>(null);
        }
        public DbSet<Account> Accountset { get; set; }
        public DbSet<dailyjob> Dailyjobset { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Account>().HasKey<int>(e => e.EmpId).Ignore(e => e.EntityId);
            modelBuilder.Entity<dailyjob>().HasKey<int>(e => e.JobId).Ignore(e => e.EntityId);
           
        }
    }
}
    