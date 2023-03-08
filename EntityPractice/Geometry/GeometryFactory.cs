using NetTopologySuite;

namespace EntityPractice.Geometry
{
    public sealed class GeometryFactory : IGeometryFactory
    {
        public NetTopologySuite.Geometries.GeometryFactory geometryFactory()
        {
            return NtsGeometryServices.Instance.CreateGeometryFactory(srid:4326);
        }
    }
}
