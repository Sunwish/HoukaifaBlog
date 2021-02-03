using System;
using System.Collections.Generic;
using System.Text;
using HoukaifaBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HoukaifaBlog.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

    }
}
