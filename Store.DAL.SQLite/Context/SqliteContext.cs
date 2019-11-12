using System;
using Microsoft.EntityFrameworkCore;
using Store.CORE.Models;

namespace Store.DAL.SQLite.Context
{
    public class SqliteContext: DbContext
    {
        private readonly string _connection;
        public DbSet<Product> Products { get; set; } 
        public SqliteContext(string connection)
        {
            if(string.IsNullOrWhiteSpace(connection)){
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
            new [] 
            {
                new Product { Id=1, Name="Test", Description="Test record", Status = true, Price = 1337 },
            });
        }
    }
}