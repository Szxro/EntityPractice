using DTOS;
using EntityPractice.Repositories.MovieRepository;
using Microsoft.AspNetCore.Mvc;

namespace EntityPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MovieController(IMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("post/autoMany")]

        public async Task<ActionResult> PostAuto(MovieDTO movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.AddAutoMovie(movie);
            return Ok();
        }

        [HttpPost("post/manualMany")]

        public async Task<ActionResult> PostManual(MovieDTO movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.AddManualMovie(movie);
            return Ok();
        }

        [HttpGet("get/manualGet")]

        public async Task<ActionResult<IEnumerable<object>>> GetManual()
        {
            return Ok(await _repository.GetMovieDTO());
        }

        [HttpGet("get/groupByGenders")]

        public async Task<ActionResult<IEnumerable<object>>> GroupByGenders()
        {
            return Ok(await _repository.GroupByMoviGender());
        }

        [HttpGet("get/FilterBy")]

        public async Task<ActionResult<List<MovieDTO>>> FilterManualByName([FromQuery] MovieFilterDTO filterDTO)
        {        
            return Ok(await _repository.FilterManualByName(filterDTO));
        }
    }
}
