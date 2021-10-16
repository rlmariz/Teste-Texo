using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Teste_Texo_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IRepositoryMovie repositoryMovie;

        public MovieController(IRepositoryMovie repositoryMovie)
        {
            this.repositoryMovie = repositoryMovie;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Content(JsonConvert.SerializeObject(repositoryMovie.GetAll()), "application/json");
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int Id)
        {
            return Content(JsonConvert.SerializeObject(repositoryMovie.Get(Id)), "application/json");
        }
        
        [HttpGet("winners")]
        public ActionResult GetWinners()
        {
            return Content(JsonConvert.SerializeObject(repositoryMovie.GetWinners()), "application/json");
        }

        [HttpGet("wins")]
        public ActionResult GetWins()
        {
            return Content(JsonConvert.SerializeObject(repositoryMovie.GetWins()), "application/json");
        }

        [HttpGet("winsstatistic")]
        public ActionResult GetWinsStatistic()
        {
            return Content(JsonConvert.SerializeObject(repositoryMovie.GetWinsStatistic()), "application/json");
        }

    }
}
