using AutoMapper;
using Context;
using DTOS;
using Models;

namespace EntityPractice.Repositories.CinemaMovieRepository
{
    public class CinemaMovieRepository : ICinemaMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CinemaMovieRepository(AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAutoMovieCinema(CinemaMovieDTO request)
        {
            _context.CinemaMovies.Add(_mapper.Map<CinemaMovie>(request));
            await _context.SaveChangesAsync();
        }
    }
}
