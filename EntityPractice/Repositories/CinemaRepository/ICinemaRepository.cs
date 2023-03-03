using DTOS;

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
    }
}
