using Context.Conversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Config
{
    public class CinemaConfig : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(x => x.CinemaType)
                   .HasConversion<CinemaTypeConversion>();

            builder.Property<DateTime>("StartDate")
                    .HasDefaultValueSql("GETDATE()")
                     .HasColumnType("date");
        }
    }
}
