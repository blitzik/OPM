using System;
using System.IO;
using Measurement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Measurement.Services.SQLite
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@$"Data Source={GetDatabaseFileLPath("db.sqlite3")}");
            
            base.OnConfiguring(optionsBuilder);
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
        

        private string GetDatabaseFileLPath(string db)
        {
            string fileLocation = Path.Combine(GetDatabaseDirectoryPath(), db);
            if (!File.Exists(fileLocation)) {
                File.Create(fileLocation);
            }

            return fileLocation;
        }
        
        
        private string GetDatabaseDirectoryPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "_opm");
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}