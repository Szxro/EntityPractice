namespace DTOS
{
    public record MovieTheaterDTO
    {
        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public double Rating { get; init; }

        public double Latitude { get; init; }

        public double Longitude { get; init; }
    }
}