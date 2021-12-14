using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Data
{
    class ProductManagerContext: DbContext
    {
        private readonly string connectionString;

        public DbSet<Product> Products { get; set; }
        public ProductManagerContext (string connectionString)
        {
            this.connectionString = connectionString;
        }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer (connectionString); 
        }
    }
}
