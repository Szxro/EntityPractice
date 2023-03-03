using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class CinemaExistingDTO
    {
        public CinemaType CinemaType { get; set; }

        public double Price { get; set; }

        public int MovieTheater { get; set; } = new();
    }
}
