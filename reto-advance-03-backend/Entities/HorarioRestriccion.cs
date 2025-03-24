using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace reto_advance_03_backend.Entities
{
    [Table("horario_restriccion")]
    public class HorarioRestriccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("dia_semana")]
        public int DiaSemana { get; set; }

        [Column("termina_placa")]
        [AllowNull]
        public string TerminaPlaca { get; set; }

        [Column("inicio")]
        public TimeSpan Inicio { get; set; }

        [Column("fin")]
        public TimeSpan Fin { get; set; }


    }
}
