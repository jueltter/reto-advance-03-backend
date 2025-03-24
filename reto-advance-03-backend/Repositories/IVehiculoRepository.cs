using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend.Repositories
{
    public interface IVehiculoRepository
    {
        Vehiculo Save(Vehiculo e);
        List<Vehiculo> FindAll(string? SearchString);
    }
}
