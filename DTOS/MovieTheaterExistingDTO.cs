using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class MovieTheaterExistingDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Rating { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public HashSet<int> Cinema { get; set; } = new();
    }
}
