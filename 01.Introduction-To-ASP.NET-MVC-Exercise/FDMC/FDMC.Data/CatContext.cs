using FDMC.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FDMC.Data
{
    public class CatContext: DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=PETAR\SQLEXPRESS;Database=Cats_MVC;Integrated Security=True");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
