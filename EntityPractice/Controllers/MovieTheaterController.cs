using Context;
using DTOS;
using EntityPractice.Repositories.MovieTheaterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTheaterController : ControllerBase
    {
        private readonly IMovieTheaterRepository _repository;

        public MovieTheaterController(IMovieTheaterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get/movieTheaterManual")]

        public async Task<ActionResult<IEnumerable<MovieTheaterDTO>>> GetManual()
        {
            return Ok(await _repository.GetMovieTheaterManual());
        }

        [HttpGet("get/movieTheaterAuto")]

        public async Task<ActionResult<IEnumerable<MovieTheaterDTO>>> GetAuto()
        {
            return Ok(await _repository.GetMovieTheaterAuto());
        }

        [HttpGet("get/getDistance")]

        public async Task<ActionResult<IEnumerable<object>>> GetDistance(double latitude,double longitude)
        {
            if (!ModelState.IsValid)
            {
               return BadRequest();
            }

            return Ok(await _repository.GetDistance(latitude, longitude));
        }

        [HttpPost("post/postManual")]

        public async Task<ActionResult> PostManual(MovieTheaterDTO movieTheater)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _repository.CreateMovieTheaterManual(movieTheater);
            return Ok();
        }

        [HttpPost("post/postAuto")]

        public async Task<ActionResult> PostAuto(MovieTheaterDTO movieTheater)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _repository.CreateMovieTheaterAuto(movieTheater);
            return Ok();    
        }
    }
}
