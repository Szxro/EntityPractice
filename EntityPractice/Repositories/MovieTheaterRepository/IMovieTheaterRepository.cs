using DTOS;

namespace EntityPractice.Repositories.MovieTheaterRepository
{
    public interface IMovieTheaterRepository
    {
        //Eager Loading (AsNoTracking => more faster queries)
        Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterManual();

        Task<IEnumerable<MovieTheaterDTO>> GetMovieTheaterAuto();

        Task<IEnumerable<object>> GetDistance(double latitude, double longitude);

        // AsTracking()

        Task CreateMovieTheaterManual(MovieTheaterDTO movieTheater);

        Task CreateMovieTheaterAuto(MovieTheaterDTO movieTheater);

        Task<object> UpdateMovieTheaterManual(int movieTheaterID,MovieTheaterDTO movieTheater);

        Task<object> DeleteManual(int movieTheatherId);

        //Explicit Loading

        Task<object> FindAMovieTheaterExplicit(int id);

        //Select Loading

        Task<IEnumerable<object>> GetMovieTheaterSelect();

        //Adding Existing Cinema
        Task AddExistingCinema(MovieTheaterExistingDTO existingDTO);

        //Updating the MovieTheater by Desconnected model
        Task<object> UpdateMovieTheaterDesconnected(MovieTheaterDTO theaterDTO, int id);
    }
}
