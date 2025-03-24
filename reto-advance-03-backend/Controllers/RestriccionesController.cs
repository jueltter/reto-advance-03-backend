using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Services;

namespace reto_advance_03_backend.Controllers
{
    [Route("restricciones")]
    [ApiController]
    public class RestriccionesController : ControllerBase
    {
        private readonly IRestriccionesService _service;
        private readonly ILogger<RestriccionesController> _logger;

        public RestriccionesController(IRestriccionesService service, ILogger<RestriccionesController> logger) 
        {
            _service = service;
            _logger = logger;
        }
        
        
        [HttpGet("")]
        public ActionResult<List<Restriccion>> FindByPlacaAndFecha([FromQuery] string? placa, [FromQuery] string? fecha)
        {
            _logger.LogInformation("placa: {Placa}, fecha: {Fecha}", placa, fecha);

            if (string.IsNullOrWhiteSpace(placa) || string.IsNullOrWhiteSpace(fecha))
            {
                return BadRequest("One of the values for placa or fecha is null or empty.");            
            }

            if (!DateTime.TryParseExact(fecha, @"yyyy'-'MM'-'dd HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAsDate)) 
            {
                return BadRequest("Invalid date format.");
            }

            if (DateTime.Now > fechaAsDate) {
                return BadRequest("The date must be in the future.");
            }

            var retorno = _service.FindByPlacaAndFecha(placa, fechaAsDate);

            return Ok(retorno);
        }
    }
}
