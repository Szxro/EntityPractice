using AutoMapper;
using AutoMapper.QueryableExtensions;
using Context;
using DTOS;
using EntityPractice.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Models;
using Models.Enum;
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
            return await _context.MovieTheaters.AsNoTracking()
                                               //Eager Loading
                                               .Include(x => x.Cinema)
                                               .Select(prop => prop.MovieTheatherAsDto())
                                               .ToListAsync();
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

            //Adding the cinema
            foreach (CinemaDTO cinema in movieTheater.Cinema)
            {
                movie.Cinema = new()
                {
                    new Cinema{CinemaType = cinema.CinemaType,Price = cinema.Price }
                };
            }

            _context.MovieTheaters.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task CreateMovieTheaterAuto(MovieTheaterDTO movieTheater)
        {
            _context.MovieTheaters.Add(_mapper.Map<MovieTheater>(movieTheater));
            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateMovieTheaterManual(int movieTheaterId, MovieTheaterDTO movieTheater)
        {
            //Getting the item from the DB (? to posible null values)
            MovieTheater? item = await _context.MovieTheaters
                                        .Where(prop => prop.Id == movieTheaterId)
                                        .FirstOrDefaultAsync();

            if (item == null)
            {
                return new { Message = "MovieTheater not found" };
            }

            //Creating the geometry instance
            var geometryInstance = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //Updating the props
            item.Name = movieTheater.Name;
            item.Description = movieTheater.Description;
            item.Rating = movieTheater.Rating;
            item.Location = geometryInstance.CreatePoint(new Coordinate(movieTheater.Latitude, movieTheater.Longitude));

            //Updating the Cinema (Dont do anything to the Cinema)
            //foreach (Cinema cinema in item.Cinema)
            //{
            //    item.Cinema = new()
            //    {
            //        new Cinema{CinemaType = cinema.CinemaType, Price = cinema.Price }
            //    };
            //}

            //Updating the item 
            _context.MovieTheaters.Update(item);

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

        public async Task<object> FindAMovieTheaterExplicit(int id)
        {
            //Need to be AsTracking
            MovieTheater? movieTheater = await _context.MovieTheaters.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (movieTheater  == null)
            {
                //returning a empty value
                return Enumerable.Empty<MovieTheaterDTO>();
            }

            //Explicit Loading (Explit the query in two to load the related data)
            await _context.Entry(movieTheater).Collection(prop => prop.Cinema).LoadAsync();

            return _mapper.Map<MovieTheaterDTO>(movieTheater);
        }

        public async Task<IEnumerable<object>> GetMovieTheaterSelect()
        {
            //Select Loading (Single Query to load the data)
            return await _context.MovieTheaters.AsNoTracking()
                .Select(
                prop => new {
                    Name = prop.Name,
                    Latitude = prop.Location.X,
                    Longitude = prop.Location.Y,
                    Cinema = prop.Cinema.Select(load =>
                    new { 
                        CinemaType = load.CinemaType,
                        Price = load.Price })
                })
                .ToListAsync();
        }
    }
}
