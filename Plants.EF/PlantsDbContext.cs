using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.EF
{
    public class PlantsDbContext : DbContext
    {
        public PlantsDbContext()
           : base(SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), "DefaultConnection")
               .Options)
        {
        }

        public PlantsDbContext(DbContextOptions<PlantsDbContext> options) : base(options)
        {
        }
    }
}
