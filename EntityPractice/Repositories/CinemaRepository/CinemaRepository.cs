using AutoMapper;
using AutoMapper.QueryableExtensions;
using Context;
using DTOS;
using EntityPractice.Geometry;
using EntityPractice.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enum;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityPractice.Repositories.CinemaRepository
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IGeometryFactory _factory;

        public CinemaRepository(AppDbContext context, IMapper mapper,IGeometryFactory factory)
        {
            _context = context;
            _mapper = mapper;
            _factory = factory;
        }

        public async Task AddCinemaManual(CinemaDTO cinema)
        {
            var geometryFactory = _factory.geometryFactory();

            Cinema cine = new()
            {
                CinemaType = cinema.CinemaType,
                Price = cinema.Price,
                MovieTheater = new MovieTheater()
                {
                    Name = cinema.MovieTheater.Name,
                    Description = cinema.MovieTheater.Description,
                    Rating = cinema.MovieTheater.Rating,
                    Location = geometryFactory.CreatePoint(new Coordinate(cinema.MovieTheater.Latitude,cinema.MovieTheater.Longitude))
                }
            };
            //Beginning a transaction is something fail is going to rollback  
            await _context.Database.BeginTransactionAsync();

            _context.Cinemas.Add(cine);
            await _context.SaveChangesAsync();

            await _context.Database.CommitTransactionAsync();
            //Is going to do the changes when the transaction is commit
        }

        public async Task<IEnumerable<CinemaDTO>> OrderCinemaAsc()
        {
            return await _context.Cinemas
                .AsNoTracking()
                .ProjectTo<CinemaDTO>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Price) //Ordering by Price (Asc)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GroupByPrice()
        {
            //Select Loading 
            return await _context.Cinemas
                                 .GroupBy(x => x.Price > 250)    
                                 .Select(prop =>
                                     new {
                                     IsGreater = prop.Key,
                                     Count = prop.Count(),
                                     Result = prop.Select(prop =>
                                     new {
                                      //Getting the key value from the enum
                                      CinemaType = prop.CinemaType.ToString(),
                                      prop.Price,
                                      MovieTheaterName = prop.MovieTheater.Name,
                                      MovieTheaterDescription = prop.MovieTheater.Description,
                                      Latitude = prop.MovieTheater.Location.X,
                                      Longitude = prop.MovieTheater.Location.Y
                                     })
                                 })
                                  .ToListAsync();
        }     

        // Loading Related Data

        public async Task<IEnumerable<CinemaDTO>> GetEagerCinema()
        {
            return await _context.Cinemas
                .AsNoTracking()
                //Eager Loading
                .Include(x => x.MovieTheater)
                .ProjectTo<CinemaDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetSelectCinema()
        {
            return await _context.Cinemas
                        .AsNoTracking()
                        //Select Loading
                        .Select(prop => new 
                        {
                           prop.Id,
                           CinemaType = prop.CinemaType.ToString(),
                           prop.Price,
                           MovieTheather = prop.MovieTheater.MovieTheatherAsDto()
                        })
                        .ToListAsync();
        }

        public async Task AddExistingMovieTheater(CinemaExistingDTO existingDTO)
        {
            Cinema cinema = _mapper.Map<Cinema>(existingDTO);

            _context.Entry(cinema.MovieTheater).State = EntityState.Unchanged;

            _context.Add(cinema);

            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateCinemaEF(int id)
        {
            Cinema? cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);

            if (cinema == null)
            {
                return new {Message = $"The cinema with id {id} was not found" };
            }

            cinema.Price += 500;

            await _context.SaveChangesAsync();

            return new {Message = "The Cinema was updated" };
        }

        public async Task<object> UpdateCinemaQuery(int id)
        {
            Cinema? cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);

            if (cinema == null)
            {
                return new { Message = $"The cinema with id {id} was not found" };
            }

            await _context.Database.BeginTransactionAsync();

            await _context.Database.ExecuteSqlInterpolatedAsync //Executing a query to improve perfomance
                ($"UPDATE CINEMAS SET Price = Price * 1.1 WHERE Id = {cinema.Id} AND CinemaType = {cinema.CinemaType}");

            await _context.SaveChangesAsync();

            await _context.Database.CommitTransactionAsync();

            return new { Message = "The Cinema was updated" };
        }
    }
}
