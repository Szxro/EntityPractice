using DTOS;
using Models;

namespace EntityPractice.Repositories.MovieRepository
{
    public interface IMovieRepository
    {
        Task AddAutoMovie(MovieDTO movie);

        Task AddManualMovie(MovieDTO movie);

        Task<IEnumerable<object>> GetMovieDTO();
    }
}
