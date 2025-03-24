using System.Diagnostics.CodeAnalysis;

namespace reto_advance_03_backend
{
    public class Restriccion
    {

        [AllowNull]
        public string Placa { get; set; }

        public DateTime Fecha { get; set; }

        public bool PuedeCircular { get; set; }


    }
}
