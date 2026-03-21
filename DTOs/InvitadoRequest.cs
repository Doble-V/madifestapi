namespace madi_fest_api.DTOs
{
    public class InvitadoRequest
    {
        public int FestId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Mensaje { get; set; }
        public bool? ConfirmaAcomp { get; set; } 
        public List<string> Acompanantes { get; set; } = new List<string>();
    }
}
