using Microsoft.AspNetCore.Mvc;
using Predikt.Contract;
using System.Threading.Tasks;

namespace Predikt.Api.Web.Controllers
{
    [Route("[controller]")]
    public class LeaguesController : Controller
    {
        private readonly IRepository _repository;

        public LeaguesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAllLeagues());
        }
    }
}
