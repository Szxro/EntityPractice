using AutoMapper;
using DTOS;
using Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityPractice.Utilities
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {

            //Simple Mapping
            CreateMap<CinemaDTO, Cinema>();
            CreateMap<Cinema, CinemaDTO>();

            CreateMap<CinemaExistingDTO, Cinema>()
                .ForMember(ent => ent.MovieTheater , prop => prop.MapFrom(dto => new MovieTheater {Id = dto.MovieTheater }));


            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieDTO, Movie>();


            CreateMap<MovieGenderDTO, MovieGenders>();

            CreateMap<CinemaMovieDTO, CinemaMovie>();

            //Mapping Spacial Data into the DTO
            var geometry = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            CreateMap<MovieTheater, MovieTheaterDTO>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(prop => prop.Location.X))
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(prop => prop.Location.Y));

            CreateMap<MovieTheaterDTO, MovieTheater>()
                .ForMember(ent => ent.Location, prop => prop.MapFrom(dto => geometry.CreatePoint(new Coordinate(dto.Latitude, dto.Longitude))));

            CreateMap<MovieTheaterExistingDTO, MovieTheater>()
                .ForMember(ent => ent.Location, prop => prop.MapFrom(dto => geometry.CreatePoint(new Coordinate(dto.Latitude, dto.Longitude))))
                .ForMember(ent => ent.Cinema, prop => prop.MapFrom(dto => dto.Cinema.Select(prop => new Cinema {Id = prop })));
        }
    }
}
