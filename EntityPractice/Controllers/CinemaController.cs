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
    }
}
