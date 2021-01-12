using Cleverbit_tech_test.Entities.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit_tech_test.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Regions> Regions { get; set; }
        public DbSet<Employees> Employees { get; set; }
      
        
    }
}
