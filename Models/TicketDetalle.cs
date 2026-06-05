namespace Garantias.API.Models
{
    using System.Text.Json.Serialization;
    public class TicketDetalle
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ComponenteId { get; set; }
        public Componente? Componente { get; set; }
        public string? TipoGarantia { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [JsonIgnore]
        public Ticket? Ticket { get; set; }
    }
}