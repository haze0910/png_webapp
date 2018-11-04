using Barista.SharedKernel.Interfaces;
using CoffeeMgt.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMgt.Infra.Data
{
    public class CoffeeMgtDbContext : DbContext, IDatabaseContext
    {
        public CoffeeMgtDbContext(DbContextOptions<CoffeeMgtDbContext> options)
           : base(options)
        {

        }

        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Pantry> Pantries { get; set; }
        public virtual DbSet<Beverage> Beverages { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }

        public async Task<int> ExecuteSqlAsync(string sqlCmd, params object[] args)
        {
            var result = await Database.ExecuteSqlCommandAsync(sqlCmd, args);
            return result;
        }
    }
}
