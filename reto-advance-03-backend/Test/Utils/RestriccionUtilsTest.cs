using System.Globalization;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Utils;
using Xunit;

namespace reto_advance_03_backend.Test.Utils
{
    public class RestriccionUtilsTest
    {
        private readonly ILogger<RestriccionUtilsTest> _logger;

        public RestriccionUtilsTest()
        {
            // Create a logger instance that writes to the console
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<RestriccionUtilsTest>();
        }

        [Fact]
        public void ValidarRestriccion_ReturnsExpectedResults()
        {
            // Act
            var horariosDisponibles = new List<HorarioRestriccion> {  new HorarioRestriccion { Id = 1, DiaSemana = 1, TerminaPlaca = "[\"1\",\"2\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 2, DiaSemana = 1, TerminaPlaca = "[\"1\",\"2\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 3, DiaSemana = 2, TerminaPlaca = "[\"3\",\"4\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 4, DiaSemana = 2, TerminaPlaca = "[\"3\",\"4\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 5, DiaSemana = 3, TerminaPlaca = "[\"5\",\"6\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 6, DiaSemana = 3, TerminaPlaca = "[\"5\",\"6\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 7, DiaSemana = 4, TerminaPlaca = "[\"7\",\"8\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 8, DiaSemana = 4, TerminaPlaca = "[\"7\",\"8\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 9, DiaSemana = 5, TerminaPlaca = "[\"9\",\"0\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 10, DiaSemana = 5, TerminaPlaca = "[\"9\",\"0\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) } };

            var fecha = "2025-03-26 07:00:00";

            DateTime.TryParseExact(fecha, @"yyyy'-'MM'-'dd HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAsDate);
            
            var result = RestriccionUtils.ValidarRestriccion(horariosDisponibles, "PBA1235", fechaAsDate, _logger);

            const bool expected = false;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
