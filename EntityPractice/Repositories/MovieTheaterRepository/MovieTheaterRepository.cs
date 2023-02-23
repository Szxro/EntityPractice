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

        public async Task<object> UpdateMovieTheaterManual(int movieTheaterId,MovieTheaterDTO movieTheater)
        {
            //Getting the item from the DB (? to posible null values)
            MovieTheater? item = await _context.MovieTheaters.Where(prop => prop.Id == movieTheaterId).FirstOrDefaultAsync();
            
            if (item  == null)
            {
                return new { Message = "MovieTheater not found" };
            }

            //Creating the geometry instance
            var geometryInstance = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //Making a copy of it
            MovieTheater movie = item with
            {
                Name = movieTheater.Name,
                Description = movieTheater.Description,
                Rating = movieTheater.Rating,
                Location = geometryInstance.CreatePoint(new Coordinate(movieTheater.Latitude,movieTheater.Longitude))
            };

            _context.MovieTheaters.Update(movie);

            await _context.SaveChangesAsync();

            return new {Message = "Update Succesfully" };
        }

        public async Task<object> DeleteManual(int movieTheatherId)
        { 
            MovieTheater? item = await _context.MovieTheaters.Where(prop => prop.Id == movieTheatherId).FirstOrDefaultAsync();

            if (item == null)
            {
                return new { Message = "MovieTheater not Found" };
            }

            _context.MovieTheaters.Remove(item);
            await _context.SaveChangesAsync();

            return new {Message = "Deleted Succesfully" };
        }
    }
}
