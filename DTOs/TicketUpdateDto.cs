namespace Garantias.API.DTOs
{
    public class TicketUpdateDto
    {
        public DateTime? FechaValidacion { get; set; }
        public DateTime? FechaGestionGarantia { get; set; }
        public string? Observacion { get; set; }
    }
}
