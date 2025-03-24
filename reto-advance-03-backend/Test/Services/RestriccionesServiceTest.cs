using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Repositories;
using reto_advance_03_backend.Services;
using reto_advance_03_backend.Test.Utils;
using reto_advance_03_backend.Utils;
using Xunit;

namespace reto_advance_03_backend.Test.Services
{
    public class RestriccionesServiceTest
    {
        private readonly ILogger<RestriccionesService> _logger;

        public RestriccionesServiceTest()
        {
            // Create a logger instance that writes to the console
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<RestriccionesService>();
        }


        [Fact]
        public void FindByPlacaAndFechaTest()
        {
            _logger.LogInformation("Empieza unit test de clase de servicio");


            // Arrange
            var mockRepo = new Mock<IHorarioRestriccionRepository>();
            mockRepo.Setup(repo => repo.FindAll()).Returns(GetSampleData());

            var service = new RestriccionesService(mockRepo.Object, _logger);

            var fecha = "2025-03-26 07:00:00";

            DateTime.TryParseExact(fecha, @"yyyy'-'MM'-'dd HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaAsDate);

            var result = service.FindByPlacaAndFecha("PBA1235", fechaAsDate).First().PuedeCircular;

            const bool expected = false;

            // Assert
            Assert.Equal(expected, result);
        }

        private List<HorarioRestriccion> GetSampleData()
        {
            return new List<HorarioRestriccion> {  new HorarioRestriccion { Id = 1, DiaSemana = 1, TerminaPlaca = "[\"1\",\"2\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 2, DiaSemana = 1, TerminaPlaca = "[\"1\",\"2\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 3, DiaSemana = 2, TerminaPlaca = "[\"3\",\"4\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 4, DiaSemana = 2, TerminaPlaca = "[\"3\",\"4\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 5, DiaSemana = 3, TerminaPlaca = "[\"5\",\"6\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 6, DiaSemana = 3, TerminaPlaca = "[\"5\",\"6\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 7, DiaSemana = 4, TerminaPlaca = "[\"7\",\"8\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 8, DiaSemana = 4, TerminaPlaca = "[\"7\",\"8\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 9, DiaSemana = 5, TerminaPlaca = "[\"9\",\"0\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 10, DiaSemana = 5, TerminaPlaca = "[\"9\",\"0\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) } };
        }
    }
}
