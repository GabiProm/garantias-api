namespace Garantias.API.DTOs
{
    public class TicketDetalleCreateDto
    {
        public int ComponenteId { get; set; }
        public string? TipoGarantia { get; set; }
        public string? Observaciones { get; set; }
    }
}