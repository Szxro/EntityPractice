using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsLive { get; set; }

        public DateTime ReleaseDate { get; set; }

        // (N:N) AUTO
        public List<MovieGenders> MovieGenders { get; set; } = new();

        //(N:N) MANUAL
        public HashSet<CinemaMovie> CinemaMovies { get; set; } = new();
    }
}
