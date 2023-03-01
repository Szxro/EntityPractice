using AutoMapper;
using Context;
using DTOS;
using EntityPractice.Utilities;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EntityPractice.Repositories.MovieRepository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAutoMovie(MovieDTO movie)
        {
            _context.Movies.Add(_mapper.Map<Movie>(movie));
            await _context.SaveChangesAsync();
        }

        public async Task AddManualMovie(MovieDTO movie)
        {
            Movie newMovie = new()
            {
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                IsLive = movie.IsLive,
            };

            //Adding the Genders
            foreach (MovieGenderDTO genders in movie.MovieGenders)
            {
                newMovie.MovieGenders = new() { new MovieGenders {Name = genders.Name } };
            }

            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetMovieDTO()
        {
            return await _context.Movies
                                 .AsNoTracking()
                                 .Select(prop => prop.MovieAsDto())
                                 .ToListAsync();
        }
    }
}
