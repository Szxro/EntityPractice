using AutoMapper;
using AutoMapper.QueryableExtensions;
using Context;
using DTOS;
using EntityPractice.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityPractice.Repositories.MovieTheaterRepository
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MovieTheaterRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<object>> GetDistance(double latitude, double longitude)
        {
            var geometry = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //Creating the user Coordinate
            var userCoordinate = geometry.CreatePoint(new Coordinate(latitude,longitude));

            return await _context.MovieTheaters
                    .AsNoTracking()
                    .OrderBy(or => or.Location.Distance(userCoordinate))
                    .Select(prop => new { Name = prop.Name, Distance = $"{prop.Location.Distance(userCoordinate)}m" })
                    .ToListAsync();
        }

        public async Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterManual()
        {
            //AsNoTracking => more faster queries
            return await _context.MovieTheaters.AsNoTracking().Select(prop => prop.AsDto()).ToListAsync();
            //Select (Project To => Object)
        }

        public async Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterAuto()
        {
            return await _context.MovieTheaters.AsNoTracking().ProjectTo<MovieTheaterDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task CreateMovieTheaterManual(MovieTheaterDTO movieTheater)
        {
            var geometry = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            MovieTheater movie = new()
            {
                Name = movieTheater.Name,
                Description = movieTheater.Description,
                Rating = movieTheater.Rating,
                Location = geometry.CreatePoint(new Coordinate(movieTheater.Latitude, movieTheater.Longitude))
            };

            _context.MovieTheaters.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task CreateMovieTheaterAuto(MovieTheaterDTO movieTheater)
        {
            _context.MovieTheaters.Add(_mapper.Map<MovieTheater>(movieTheater));
            await _context.SaveChangesAsync();
        }
    }
}
