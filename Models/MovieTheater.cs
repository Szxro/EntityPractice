using NetTopologySuite.Geometries;

namespace Models
{
    //Record is use for inmutable data in this case model
    public class MovieTheater
    {
        public int Id { get; set; } // init is use to set the data and not make a huge ctor

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;  

        public double Rating { get; set; }

        public Point Location { get; set; } = null!;

        //(1:N)
        public HashSet<Cinema> Cinema { get; set; } = new();

    }
}