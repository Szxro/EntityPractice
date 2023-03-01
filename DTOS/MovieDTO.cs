using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class MovieDTO
    {
        public string Name { get; set; } = null!;

        public bool IsLive { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<MovieGenderDTO> MovieGenders { get; set; } = new();
    }
}
