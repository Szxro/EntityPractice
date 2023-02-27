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
    }
}
