using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MovieGenders
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        //(N:N) AUTO
        public List<Movie> Movies { get; set; } = new();
    }
}
