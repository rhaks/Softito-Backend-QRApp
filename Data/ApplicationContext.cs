using System;
using Microsoft.EntityFrameworkCore;
using QRMenuUygulama.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QRMenuUygulama.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<State>? States { get; set; }
        public DbSet<Food>? Foods { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Restaurant>? Restaurants { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}

