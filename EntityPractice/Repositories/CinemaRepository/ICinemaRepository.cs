using DTOS;
using Models;

namespace EntityPractice.Repositories.CinemaRepository
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<CinemaDTO>> OrderCinemaAsc();

        Task<IEnumerable<object>> GroupByPrice();

        Task AddCinemaManual(CinemaDTO cinema);

        Task<IEnumerable<CinemaDTO>> GetEagerCinema();

        Task<IEnumerable<object>> GetSelectCinema();

        Task AddExistingMovieTheater(CinemaExistingDTO existingDTO);

        Task<object> UpdateCinemaEF(int id);

        Task<object> UpdateCinemaQuery(int id);

        Task<IEnumerable<CinemaWithoutPK>> GetCinemaWithoutPKs();

        Task<IEnumerable<CinemaView>> GetCinemaView();

        Task<object?> GetCinemaById(int id);
    }
}
