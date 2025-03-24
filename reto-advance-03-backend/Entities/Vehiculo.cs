using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace reto_advance_03_backend.Entities
{
    [Table("vehiculo")]
    public class Vehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("placa")]
        [AllowNull]
        public string Placa { get; set; }

        [Column("color")]
        [AllowNull]
        public string Color { get; set; }

        [Column("modelo")]
        [AllowNull]
        public string Modelo { get; set; }

        [Column("chasis")]
        [AllowNull]
        public string Chasis { get; set; }

        [Column("kilometraje")]
        public double Kilometraje { get; set; }

        [Column("anio_modelo")]
        public int AnioModelo { get; set; }

        [Column("marca")]
        [AllowNull]
        public string Marca { get; set; }


    }
}
