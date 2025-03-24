using reto_advance_03_backend.Data;
using reto_advance_03_backend.Entities;

namespace reto_advance_03_backend.Repositories
{
    public class HorarioRestriccionRepository : IHorarioRestriccionRepository
    {
        private readonly MyDbContext _context;

        public HorarioRestriccionRepository(MyDbContext context) 
        {
            _context = context;
        }
        
        public List<HorarioRestriccion> FindAll()
        {
            return _context.HorariosRestriccion.ToList();
        }
    }
}
