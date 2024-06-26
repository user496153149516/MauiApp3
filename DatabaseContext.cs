using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Goal> Goals { get; set; } = null!;
        public DbSet<Plan> Plans { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Profit> Profits { get; set; } = null!;
        public DbSet<Cost> Costs { get; set; } = null!;
        public DatabaseContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //stackoverflow.com/questions/72598734/entity-framework-core-in-net-maui-xamarin-forms
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "base.db");
            //optionsBuilder.UseSqlite($"Filename={dbPath}");
            optionsBuilder.UseSqlite("Data Source = D:\\Base.db");
        }
    }
}
