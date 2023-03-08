using AutoMapper;
using AutoMapper.QueryableExtensions;
using Context;
using DTOS;
using EntityPractice.Geometry;
using EntityPractice.Utilities;
using Microsoft.EntityFrameworkCore;
using Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityPractice.Repositories.MovieTheaterRepository
{
    public class MovieTheaterRepository : IMovieTheaterRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGeometryFactory _factory;

        public MovieTheaterRepository(AppDbContext context,IMapper mapper,IGeometryFactory factory)
        {
            _context = context;
            _mapper = mapper;
            _factory = factory;
        }
        public async Task<IEnumerable<object>> GetDistance(double latitude, double longitude)
        {
            var geometry = _factory.geometryFactory();

            //Creating the user Coordinate
            Point userCoordinate = geometry.CreatePoint(new Coordinate(latitude,longitude));

            return await _context.MovieTheaters
                    .AsNoTracking()
                    .OrderBy(or => or.Location.Distance(userCoordinate))
                    .Select(prop => new { prop.Name, Distance = $"{prop.Location.Distance(userCoordinate)}m" })
                    .ToListAsync();
        }

        public async Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterManual()
        {
            //AsNoTracking => more faster queries
            return await _context.MovieTheaters.AsNoTracking()
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
            var geometry = _factory.geometryFactory();

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
            var geometryInstance = _factory.geometryFactory();

            //Updating the props
            item.Name = movieTheater.Name;
            item.Description = movieTheater.Description;
            item.Rating = movieTheater.Rating;
            item.Location = geometryInstance.CreatePoint(new Coordinate(movieTheater.Latitude, movieTheater.Longitude));


            _context.MovieTheaters.Update(item);

            await _context.SaveChangesAsync();

            //This also call using connected Model
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
            //Explicit Loading requires a collection of data (IEnumerable,List,etc..)

            return _mapper.Map<MovieTheaterDTO>(movieTheater);
        }

        public async Task<IEnumerable<object>> GetMovieTheaterSelect()
        {
            //Select Loading (Single Query to load the data)
            return await _context.MovieTheaters.AsNoTracking()
                .Select(
                prop => new
                {
                    prop.Name,
                    Latitude = prop.Location.X,
                    Longitude = prop.Location.Y,
                    prop.Description,
                    Cinema = prop.Cinema.Select(ent=> new 
                    {
                        ent.CinemaType,
                        ent.Price
                    })
                })
                .ToListAsync();
        }

        public async Task AddExistingCinema(MovieTheaterExistingDTO existingDTO)
        {
            MovieTheater movieTheater = _mapper.Map<MovieTheater>(existingDTO);
            //Adding Existing Cinema
            foreach (var existingCinema in movieTheater.Cinema)
            {
                //The default state of EF is Added 
                _context.Entry(existingCinema).State = EntityState.Unchanged;
            }

            _context.Add(movieTheater);
            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateMovieTheaterDesconnected(MovieTheaterDTO theaterDTO,int id)
        {
            bool isInDB = await _context.MovieTheaters.AnyAsync(x => x.Id == id);
            //return true or false is the movieTheather or not.

            if (!isInDB)
            {
                return new {Message = $"The MovieTheater with the id {id} was not found" };
            }

            MovieTheater movieTheater = _mapper.Map<MovieTheater>(theaterDTO); 
            movieTheater.Id = id;//changing the id to the id given

            _context.Update(movieTheater); //changing the state from added to modified 
            //update the whole props of the entity

            await _context.SaveChangesAsync();

            return new { Message = "The MovieTheater was updated" };
        }
    }
}
