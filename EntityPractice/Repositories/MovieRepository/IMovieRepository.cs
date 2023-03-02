using DTOS;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace EntityPractice.Repositories.MovieRepository
{
    public interface IMovieRepository
    {
        Task AddAutoMovie(MovieDTO movie);

        Task AddManualMovie(MovieDTO movie);

        Task<IEnumerable<object>> GetMovieDTO();

        Task<IEnumerable<object>> GroupByMoviGender();

        Task<List<MovieDTO>> FilterManualByName([FromQuery] MovieFilterDTO filterDTO);
    }
}
