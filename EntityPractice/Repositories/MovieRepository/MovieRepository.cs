using AutoMapper;
using Context;
using DTOS;
using EntityPractice.Utilities;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<MovieDTO>> FilterManualByName([FromQuery] MovieFilterDTO filterDTO)
        {
            //AsQueryable is to create a query by parts (this call deferred execution)
            var MoviesAsQueryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filterDTO.Name))
            {
                //Building the query
                MoviesAsQueryable = MoviesAsQueryable.Where(prop => prop.Name.Contains(filterDTO.Name));
            }

            //Eager Loading to Include the MovieGenders
            List<Movie> newMovies = await MoviesAsQueryable.Include(x => x.MovieGenders).ToListAsync();

            //Creating a List
            List<MovieDTO> movieDTOs = new();

            //Adding the props of the Movie and genders
            foreach (Movie movie in newMovies)
            {
                movieDTOs.Add(
                    new MovieDTO {
                    Name = movie.Name,
                    IsLive = movie.IsLive,
                    ReleaseDate = movie.ReleaseDate,
                    MovieGenders = movie.MovieGenders.Select(x => x.GenderAsDTO()).ToList(),
                });
            }

            return movieDTOs;
        }

        public async Task<IEnumerable<object>> GetMovieDTO()
        {
            return await _context.Movies
                                 .AsNoTracking()
                                 .Select(prop => prop.MovieAsDto())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<object>> GroupByMoviGender()
        {
            return await _context.Movies
                                 .GroupBy(prop => prop.MovieGenders.Count())
                                 .Select(prop => new
                                 {
                                     //Numbers of gender
                                     Count = prop.Key,
                                     //Acceding a Movie
                                     Title = prop.Select( x=> new {x.Name}),
                                     //Acceding a MovieGenders and Showing the genders name
                                     Genders = prop.Select(x => x.MovieGenders.Select(prop=> new { prop.Name}))
                                 }).ToListAsync();

        }
    }
}
