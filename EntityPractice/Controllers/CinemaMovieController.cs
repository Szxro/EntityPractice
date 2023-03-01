using DTOS;
using EntityPractice.Repositories.CinemaMovieRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaMovieController : ControllerBase
    {
        private readonly ICinemaMovieRepository _repository;

        public CinemaMovieController(ICinemaMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("post/autoManyManual")]

        public async Task<ActionResult> AddManualMany(CinemaMovieDTO request)
        {
            await _repository.AddAutoMovieCinema(request);
            return Ok();
        }
    }
}
