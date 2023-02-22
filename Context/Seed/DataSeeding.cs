using Microsoft.EntityFrameworkCore;
using Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Seed
{
    public class DataSeeding
    {
        /// <summary>
        /// Simple Data Seeding (Setting default data)
        /// </summary>
        /// <param name="builder"></param>
        public static void SetData(ModelBuilder builder)
        {
            //Creating the Geometry Instance to create the point
            var geometry = NtsGeometryServices.Instance.CreateGeometryFactory(srid:4326);
            MovieTheater movieTheater = new() {
                Id = 1,
                Name = "Sebastian MovieTheater", 
                Description = "Great", 
                Rating = 6.0, 
                Location = geometry.CreatePoint(new Coordinate(18.474220980395156, -69.93091285173654)) 
            };

            builder.Entity<MovieTheater>().HasData(movieTheater);        
        }
    }
}
