using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class CinemaMovieDTO
    {
        public CinemaDTO Cinema { get; set; } = new();

        public MovieDTO Movie { get; set; } = new();

        public bool isThereAnyTickets { get; set; }
    }
}
