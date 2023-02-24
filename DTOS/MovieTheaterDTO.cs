namespace DTOS
{
    public class MovieTheaterDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Rating { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public HashSet<CinemaDTO> Cinema { get; set; } = new();
    }
}