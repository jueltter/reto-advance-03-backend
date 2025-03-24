using reto_advance_03_backend.Data;
using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly MyDbContext _context;

        public VehiculoRepository(MyDbContext context)
        {
            _context = context;
        }
        
        public List<Vehiculo> FindAll(string? SearchString)
        {
            if (string.IsNullOrWhiteSpace(SearchString)) {
                return _context.Vehiculos.ToList();
            }

            return _context.Vehiculos
                .Where(v => v.Placa.StartsWith(SearchString))
                .ToList();
        }

        public Vehiculo Save(Vehiculo e)
        {
            _context.Vehiculos.Add(e);
            _context.SaveChanges();
            return e;
        }
    }
}
