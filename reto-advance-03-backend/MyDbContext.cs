using Microsoft.EntityFrameworkCore;
using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<HorarioRestriccion> HorariosRestriccion { get; set; }
    }
}
