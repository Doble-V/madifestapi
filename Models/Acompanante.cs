using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace madi_fest_api.Models
{
    [Table("MF_COMPANIONS")]
    public class Acompanante
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Nombre { get; set; } = null!;

        [Column("GUEST_ID")]
        public int InvitadoId { get; set; }

        [ForeignKey("InvitadoId")]
        public Invitado Invitado { get; set; } = null!;
    }
}
