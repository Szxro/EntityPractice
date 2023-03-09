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
    public class CinemaWithOutPkConfig : IEntityTypeConfiguration<CinemaWithoutPK>
    {
        public void Configure(EntityTypeBuilder<CinemaWithoutPK> builder)
        {
            builder.HasNoKey().ToSqlQuery("SELECT CinemaType,Price From Cinemas").ToView(null);
            //To View(null) => its for not use a View or Create a Table
        }
    }
}
