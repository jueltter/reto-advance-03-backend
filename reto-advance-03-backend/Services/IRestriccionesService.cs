namespace reto_advance_03_backend.Services
{
    public interface IRestriccionesService
    {
        List<Restriccion> FindByPlacaAndFecha(string Placa, DateTime fecha);
    }
}
