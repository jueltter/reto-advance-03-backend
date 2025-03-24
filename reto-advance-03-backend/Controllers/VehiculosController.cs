using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_advance_03_backend.Data;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Repositories;

namespace reto_advance_03_backend.Controllers
{
    [Route("vehiculos")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoRepository _repo;

        public VehiculosController(IVehiculoRepository repo) {
            _repo = repo;
        }

        [HttpGet("")]
        public ActionResult<List<Vehiculo>> FindAll([FromQuery(Name = "placa")] string? SearchString)
        {
            return Ok(_repo.FindAll(SearchString));
        }

        [HttpPost("")]
        public ActionResult<List<Vehiculo>> Save(Vehiculo v)
        {
            return Ok(_repo.Save(v));
        }
    }
}
