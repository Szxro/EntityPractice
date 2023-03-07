using DTOS;
using EntityPractice.Repositories.CinemaRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaRepository _repository;

        public CinemaController(ICinemaRepository repository)
        {
            _repository = repository;
        }

        //Order By and Group Actions

        [HttpGet("get/orderBy")]

        public async Task<ActionResult<IEnumerable<CinemaDTO>>> OrderByPriceAsc()
        {
            return Ok(await _repository.OrderCinemaAsc());
        }

        [HttpGet("get/groupBy")]

        public async Task<ActionResult<IEnumerable<object>>> GroupByPrice()
        {
            return Ok(await _repository.GroupByPrice());
        }

        //Post Actions

        [HttpPost("post/manualPost")]

        public async Task<ActionResult> PostCinemaManual(CinemaDTO cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(cinema);
            }
            await _repository.AddCinemaManual(cinema);
            return Ok();
        }


        // Loading Related Data

        [HttpGet("get/eagerLoading")]

        public async Task<ActionResult<IEnumerable<CinemaDTO>>> GetEagerCinema()
        {
            return Ok(await _repository.GetEagerCinema());
        }

        [HttpGet("get/selectLoading")]

        public async Task<ActionResult<IEnumerable<object>>> GetSelectCinema()
        {
            return Ok(await _repository.GetSelectCinema());
        }

        [HttpPost("post/AddExistingData")]

        public async Task<ActionResult> AddExistingData(CinemaExistingDTO existingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(existingDTO);
            }

            await _repository.AddExistingMovieTheater(existingDTO);
            return Ok();
        }

        [HttpPut("update/cinemaEF/{id:int}")]

        public async Task<ActionResult> UpdateCinemaEF(int id)
        { 
            return Ok(await _repository.UpdateCinemaEF(id));
        }


        [HttpPut("update/cinemaQuery/{id:int}")]
        public async Task<ActionResult> UpdateCinemaQuery(int id)
        {
            return Ok(await _repository.UpdateCinemaQuery(id));
        }
    }
}
