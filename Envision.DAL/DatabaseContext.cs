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
    }
}
