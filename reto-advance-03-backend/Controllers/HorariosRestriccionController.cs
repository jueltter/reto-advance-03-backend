using Microsoft.AspNetCore.Mvc;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Repositories;

namespace reto_advance_03_backend.Controllers
{
    [Route("horarios")]
    [ApiController]
    public class HorariosRestriccionController : ControllerBase
    {
        private readonly IHorarioRestriccionRepository _repo;
        private readonly ILogger<HorariosRestriccionController> _logger;

        public HorariosRestriccionController(IHorarioRestriccionRepository repo, ILogger<HorariosRestriccionController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("")]
        public ActionResult<List<HorarioRestriccion>> FindAll()
        {
            return Ok(_repo.FindAll());
        }
    }
}
