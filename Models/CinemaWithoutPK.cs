using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CinemaWithoutPK
    {
        public string CinemaType { get; set; } = null!;

        public double Price { get; set; }
    }
}
