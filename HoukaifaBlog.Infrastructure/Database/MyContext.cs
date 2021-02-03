using System;
using System.Collections.Generic;
using System.Text;
using HoukaifaBlog.Core.Entities;
using HoukaifaBlog.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HoukaifaBlog.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }

        public DbSet<Post> Posts { get; set; }

    }
}
