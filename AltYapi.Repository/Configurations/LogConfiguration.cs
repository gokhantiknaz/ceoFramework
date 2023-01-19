using AltYapi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Repository.Configurations
{
    internal class LogConfiguration:IEntityTypeConfiguration<Log>
    {

        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.MachineName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Logged).IsRequired();
            builder.Property(x => x.Level).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Message).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.Logger).HasMaxLength(250);
            builder.Property(x => x.Callsite).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Exception).HasColumnType("nvarchar(max)");
            builder.ToTable("Log");
        }
    }
}
