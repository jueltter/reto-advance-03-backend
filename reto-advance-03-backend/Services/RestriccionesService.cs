
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using Newtonsoft.Json;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Repositories;
using reto_advance_03_backend.Utils;

namespace reto_advance_03_backend.Services
{
    public class RestriccionesService : IRestriccionesService
    {
        private readonly IHorarioRestriccionRepository _repo;
        private readonly ILogger<RestriccionesService> _logger;

        public RestriccionesService(IHorarioRestriccionRepository repo, ILogger<RestriccionesService> logger) 
        {
            _repo = repo;
            _logger = logger;
        }
        
        public List<Restriccion> FindByPlacaAndFecha(string Placa, DateTime Fecha)
        {
            var horariosRestriccion = _repo.FindAll();
            
            var PuedeCircular = RestriccionUtils.ValidarRestriccion(horariosRestriccion, Placa, Fecha, _logger);

            var restriccion = new Restriccion
            {
                Placa = Placa,
                Fecha = Fecha,
                PuedeCircular = PuedeCircular
            };

            return new List<Restriccion> { restriccion };
        }


    }

    
}
