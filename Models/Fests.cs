using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace madi_fest_api.Models
{
    [Table("MF_FESTS")]
    public class Fest
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Nombre { get; set; } = null!;

        [Column("EVENT_DATE")]
        public DateTime? FechaEvento { get; set; }

        public List<Invitado> Invitados { get; set; } = new();
    }
}
