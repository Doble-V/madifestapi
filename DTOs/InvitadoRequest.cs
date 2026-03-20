namespace madi_fest_api.DTOs
{
    public class InvitadoRequest
    {
        public int FestId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Mensaje { get; set; }
        public List<string>? Acompanantes { get; set; }
    }
}
