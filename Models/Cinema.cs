using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public CinemaType CinemaType { get; set; }

        public double Price { get; set; }

        //FK (1:N)
        public int MovieTheaterId { get; set; }

        public MovieTheater MovieTheater { get; set; } = new();

        //(N:N) MANUAL
        public HashSet<CinemaMovie> CinemaMovies { get; set; } = new();
    }
}
