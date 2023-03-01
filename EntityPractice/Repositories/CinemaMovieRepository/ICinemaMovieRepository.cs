using DTOS;

namespace EntityPractice.Repositories.CinemaMovieRepository
{
    public interface ICinemaMovieRepository
    {
        Task AddAutoMovieCinema(CinemaMovieDTO request);
    }
}
