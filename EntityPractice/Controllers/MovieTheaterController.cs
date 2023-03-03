using Context;
using DTOS;
using EntityPractice.Repositories.MovieTheaterRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

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

        [HttpPut("update/updateManual")]

        public async Task<ActionResult<object>> UpdateManual(int movieTheaterId,MovieTheaterDTO movieTheater)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _repository.UpdateMovieTheaterManual(movieTheaterId, movieTheater));
        }

        [HttpDelete("delete/deleteManual")]

        public async Task<ActionResult<object>> DeleteManual(int movieTheaterId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _repository.DeleteManual(movieTheaterId));
        }

        [HttpGet("get/explicitLoading")]

        public async Task<ActionResult<object>> getExplicitLoading(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result =  await _repository.FindAMovieTheaterExplicit(id);

            return Ok(result);  
        }

        [HttpGet("get/selectLoading")]

        public async Task<ActionResult<IEnumerable<object>>> getSelectLoading()
        {
            return Ok(await _repository.GetMovieTheaterSelect());
        }

        [HttpPost("post/postingExistingData")]

        public async Task<ActionResult> PostExistingData(MovieTheaterExistingDTO existingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(existingDTO);
            }
            await _repository.AddExistingCinema(existingDTO);
            return Ok();
        }
    }
}
