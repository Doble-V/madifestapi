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
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!; // Cambiado de Nombre a Name

        [Column("EVENT_DATE")]
        public DateTime? EventDate { get; set; } // Cambiado de FechaEvento a EventDate

        // Relación con los invitados
        public List<Invitado> Guests { get; set; } = new(); // Cambiado de Invitados a Guests
    }
}