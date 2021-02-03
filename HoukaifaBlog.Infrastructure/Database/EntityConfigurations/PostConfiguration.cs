using System;
using System.Collections.Generic;
using System.Text;
using HoukaifaBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoukaifaBlog.Infrastructure.Database.EntityConfigurations
{
    class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Author).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Body).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.Remark).HasMaxLength(200);
        }
    }
}
