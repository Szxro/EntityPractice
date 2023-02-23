using DTOS;

namespace EntityPractice.Repositories.MovieTheaterRepository
{
    public interface IMovieTheaterRepository
    {
        Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterManual();

        Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterAuto();

        Task<IEnumerable<object>> GetDistance(double latitude, double longitude);

        Task CreateMovieTheaterManual(MovieTheaterDTO movieTheater);

        Task CreateMovieTheaterAuto(MovieTheaterDTO movieTheater);

        Task<object> UpdateMovieTheaterManual(int movieTheaterID,MovieTheaterDTO movieTheater);

        Task<object> DeleteManual(int movieTheatherId);
    }
}
