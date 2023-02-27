using DTOS;

namespace EntityPractice.Repositories.CinemaRepository
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<CinemaDTO>> OrderCinemaAsc();

        Task<IEnumerable<object>> GroupByPrice();
    }
}
