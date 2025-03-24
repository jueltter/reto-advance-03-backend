
using System.Reflection.Emit;
using Newtonsoft.Json;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Repositories;

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
            var localDateTime = Fecha.ToLocalTime();
            var DiaSemana = localDateTime.DayOfWeek;
            var HoraYMinutos = localDateTime.TimeOfDay;
            var UltimoDigitoPlaca = Placa[^1].ToString();

            _logger.LogInformation("fechaLocal: {Fecha}, diaSemana: {DiaSemana}, diaSemana: {DiaSemana}, horaYMinutos: {HoraYMinutos}, ultimoDigitoPlaca: {UltimoDigitoPlaca}", Fecha, DiaSemana, (int) DiaSemana, HoraYMinutos, UltimoDigitoPlaca);

            var HorariosRestriccion = _repo.FindAll();

            _logger.LogInformation("horariosRestriccion: {HorariosRestriccion}", HorariosRestriccion);

            var PuedeCircular = HorariosRestriccion.All(horarioRestriccion => {
                List<string> TerminaPlaca = JsonConvert.DeserializeObject<List<string>>(horarioRestriccion.TerminaPlaca);
                TerminaPlaca = TerminaPlaca != null ? TerminaPlaca : Enumerable.Empty<string>().ToList();
                
                return horarioRestriccion.DiaSemana == ((int)DiaSemana)
                && (horarioRestriccion.Inicio <= HoraYMinutos)
                && (horarioRestriccion.Fin >= HoraYMinutos)
                && (TerminaPlaca.Contains(UltimoDigitoPlaca))
                ;
            });

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
