using System;
using Microsoft.EntityFrameworkCore;

namespace MiniBlogSQLServerDBExample.Models
{
    
    public class MvcPostContext: DbContext
    {

        public MvcPostContext(DbContextOptions<MvcPostContext> options)
            : base(options)
        {
        }

        public DbSet<MiniBlogSQLServerDBExample.Models.Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}