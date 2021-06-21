using Envision.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Envision.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DbSet<AircraftLog> AircraftLogs { get; set; }
        public DbSet<AircraftLogImage> AircraftLogImages { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Data Source=KUSHAN-PC\MSSQLSERVER2019;Initial Catalog=EnvisionDB;Integrated Security=True");
        //        //optionsBuilder.UseSqlServer(@"Data Source=KUSHAN-PC\MSSQLSERVER2019;Initial Catalog=EnvisionDB;persist security info=True;user id=sa;password=abc@123;");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
