using AutoMapper;
using AutoMapper.QueryableExtensions;
using Context;
using DTOS;
using Microsoft.EntityFrameworkCore;
using Models.Enum;

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

        public async Task<IEnumerable<object>> GroupByPrice()
        {
            return await _context.Cinemas
                                 .GroupBy(x => x.Price > 250)    
                                 .Select(prop =>
                                     new {
                                     IsNotNull = prop.Key,
                                     Count = prop.Count(),
                                     Result = prop.ToList().Select(prop =>
                                     new {
                                      //Getting the key value from the enum
                                      CinemaType = prop.CinemaType.ToString(),
                                      Price = prop.Price,
                                      MovieTheaterName = prop.MovieTheater.Name,
                                      MovieTheaterDescription = prop.MovieTheater.Description,
                                      Latitude = prop.MovieTheater.Location.X,
                                      Longitude = prop.MovieTheater.Location.Y
                                     })
                                 })
                                  .ToListAsync();
        }

        public async Task<IEnumerable<CinemaDTO>> OrderCinemaAsc()
        {
            return await _context.Cinemas
                .AsNoTracking()
                .ProjectTo<CinemaDTO>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Price) //Ordering by Price (Asc)
                .ToListAsync();
        }
    }
}
