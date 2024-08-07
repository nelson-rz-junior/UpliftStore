﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data
{
    // MIGRATIONS
    // cd .\UpliftStore.DataAccess
    // dotnet ef migrations add InitialConfiguration --context ApplicationDbContext --startup-project D:\Projects\UpliftStore\UpliftStore\UpliftStore.csproj
    // dotnet ef database update --context ApplicationDbContext --startup-project D:\Projects\UpliftStore\UpliftStore\UpliftStore.csproj

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Frequency> Frequencies { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<WebImage> WebImages { get; set; }
    }
}
