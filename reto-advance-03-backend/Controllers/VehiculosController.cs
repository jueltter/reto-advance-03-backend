using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend.Controllers
{
    [Route("")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly MyDbContext _context;

        public VehiculosController(MyDbContext context) {
            _context = context;
        }

        [HttpGet("vehiculos")]
        public ActionResult<List<Vehiculo>> GetAll()
        {
            return Ok(_context.Vehiculos.ToList());
        }
    }
}
