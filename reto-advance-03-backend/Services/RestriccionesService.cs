
using System.Diagnostics.CodeAnalysis;
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

            var horariosRestriccion = _repo.FindAll();
            
            //_logger.LogInformation("horariosRestriccion: {horariosRestriccionFiltrados}", horariosRestriccionFiltrados);

            var horariosRestriccionFiltradosPorPlaca = horariosRestriccion.Where(horarioRestriccion => {
                List<string> TerminaPlaca = JsonConvert.DeserializeObject<List<string>>(horarioRestriccion.TerminaPlaca);
                return TerminaPlaca.Contains(UltimoDigitoPlaca);
            }).ToList();

            _logger.LogInformation("horariosRestriccionFiltradosPorPlaca: {horariosRestriccionFiltradosPorPlaca}", horariosRestriccionFiltradosPorPlaca);

            var horariosRestriccionFiltradosPorPlacaYDia = horariosRestriccionFiltradosPorPlaca.Where(horarioRestriccion => horarioRestriccion.DiaSemana == ((int)DiaSemana)).ToList();

            _logger.LogInformation("horariosRestriccionFiltradosPorPlacaYDia: {horariosRestriccionFiltradosPorPlacaYDia}", horariosRestriccionFiltradosPorPlacaYDia);

            var results = horariosRestriccionFiltradosPorPlacaYDia.ConvertAll(horarioRestriccion => {
                _logger.LogInformation("id: {id}, horarioRestriccion: {horarioRestriccion}", horarioRestriccion.Id, horarioRestriccion);
                
                var esDespuesOIgualQueInicio = TimeSpan.Compare(HoraYMinutos, horarioRestriccion.Inicio) >= 0;

                var esAntesOIgualQueFin = TimeSpan.Compare(HoraYMinutos,horarioRestriccion.Fin) <=0;

                var noEsEntre = !(esDespuesOIgualQueInicio && esAntesOIgualQueFin);
                
                _logger.LogInformation("noEsEntre: {noEsEntre}, esDespuesOIgualQueInicio: {esDespuesOIgualQueInicio}, esAntesOIgualQueFin: {esAntesOIgualQueFin}", noEsEntre, esDespuesOIgualQueInicio, esAntesOIgualQueFin);

                return noEsEntre;
            });

            _logger.LogInformation("results: {results}", results);

            var PuedeCircular = results.All(result => result);

            var restriccion = new Restriccion
            {
                Placa = Placa,
                Fecha = Fecha,
                PuedeCircular = PuedeCircular
            };

            return new List<Restriccion> { restriccion };
        }

        public class Auxiliar
        {
            [AllowNull]
            List<String> Placas { get; set; }
            [AllowNull]
            HorarioRestriccion HorarioRestriccion { get; set; }
        }


    }

    
}
