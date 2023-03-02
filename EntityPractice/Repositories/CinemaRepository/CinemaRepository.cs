using AutoMapper;
using AutoMapper.QueryableExtensions;
using Context;
using DTOS;
using EntityPractice.Utilities;
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

        public CinemaRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCinemaManual(CinemaDTO cinema)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid:4326);

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

            _context.Cinemas.Add(cine);
            await _context.SaveChangesAsync();
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
                           CinemaType = prop.CinemaType.ToString(),
                           prop.Price,
                           MovieTheather = prop.MovieTheater.MovieTheatherAsDto()
                        })
                        .ToListAsync();
        }

    }
}
