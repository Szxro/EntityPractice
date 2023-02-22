using NetTopologySuite.Geometries;

namespace Models
{
    //Record is use for inmutable data in this case model
    public record MovieTheater
    {
        public int Id { get; init; } // init is use to set the data and not make a huge ctor

        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;  

        public double Rating { get; init; }

        public Point Location { get; init; } = null!;

    }
}