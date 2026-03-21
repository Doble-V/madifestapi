using madi_fest_api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("MF_GUESTS")]
public class Invitado
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("FEST_ID")]
    public int FestId { get; set; }

    [Column("NAME")] // Coincide con [NAME] en SQL
    public string Name { get; set; } = null!;

    [Column("LAST_NAME")] // ¡ESTO CORRIGE EL ERROR DE LastName!
    public string LastName { get; set; } = null!;

    [Column("MESSAGE")]
    public string? Message { get; set; }

    [Column("CONFIRMED_COMPANIONS")] // ¡ESTO CORRIGE EL ERROR DE ConfirmedCompanions!
    public bool? ConfirmedCompanions { get; set; }

    [Column("CONFIRMED")]
    public bool Confirmed { get; set; } = true;

    [Column("DATE_REGISTER")] // ¡ESTO CORRIGE EL ERROR DE DateRegister!
    public DateTime DateRegister { get; set; } = DateTime.Now;

    // Relaciones
    [ForeignKey("FestId")]
    public virtual Fest Fest { get; set; } = null!;

    public List<Acompanante> Acompanantes { get; set; } = new();
}