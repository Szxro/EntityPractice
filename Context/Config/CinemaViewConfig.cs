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
    public class CinemaViewConfig : IEntityTypeConfiguration<CinemaView>
    {
        public void Configure(EntityTypeBuilder<CinemaView> builder)
        {
            builder.HasNoKey().ToView("CINEMA_VIEW");
        }
    }
}
