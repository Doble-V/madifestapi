using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace madi_fest_api.Models
{

    [Table("MF_GUESTS")]
    public class Invitado
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Nombre { get; set; } = null!;

        [Column("MESSAGE")]
        public string? Mensaje { get; set; }

        [Column("CONFIRMED")]
        public bool Confirmado { get; set; }

        [Column("DATE_REGISTER")]
        public DateTime? FechaRegistro { get; set; }

        [Column("FEST_ID")]
        public int FestId { get; set; }

        [ForeignKey("FestId")]
        public Fest Fest { get; set; } = null!;

        public List<Acompanante> Acompanantes { get; set; } = new();
    }

}
