using Newtonsoft.Json;
using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend.Utils
{
    public static class RestriccionUtils
    {

        public static bool ValidarRestriccion(List<HorarioRestriccion> horariosRestriccion, string Placa, DateTime Fecha, ILogger _logger)
        {
            var localDateTime = Fecha;
            var DiaSemana = localDateTime.DayOfWeek;
            var HoraYMinutos = localDateTime.TimeOfDay;
            var UltimoDigitoPlaca = Placa[^1].ToString();

            _logger.LogInformation("fechaLocal: {Fecha}, diaSemana: {DiaSemana}, diaSemana: {DiaSemana}, horaYMinutos: {HoraYMinutos}, ultimoDigitoPlaca: {UltimoDigitoPlaca}", Fecha, DiaSemana, (int)DiaSemana, HoraYMinutos, UltimoDigitoPlaca);

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

                var esAntesOIgualQueFin = TimeSpan.Compare(HoraYMinutos, horarioRestriccion.Fin) <= 0;

                var noEsEntre = !(esDespuesOIgualQueInicio && esAntesOIgualQueFin);

                _logger.LogInformation("noEsEntre: {noEsEntre}, esDespuesOIgualQueInicio: {esDespuesOIgualQueInicio}, esAntesOIgualQueFin: {esAntesOIgualQueFin}", noEsEntre, esDespuesOIgualQueInicio, esAntesOIgualQueFin);

                return noEsEntre;
            });

            _logger.LogInformation("results: {results}", results);

            return results.All(result => result);

        }
    }
}
