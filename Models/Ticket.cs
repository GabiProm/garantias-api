namespace Garantias.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? NroInventario { get; set; }
        public string? Serie { get; set; }
        public string? TicketRimac { get; set; }
        public string? NroCaso { get; set; }
        public string? Problema { get; set; }
        public DateTime FechaReporte { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public bool ProcedeGarantia { get; set; }
        public TipoDanoEnum TipoDano { get; set; }
        public DateTime? FechaGestionGarantia { get; set; }
        public string? Observacion { get; set; }

        public List<TicketDetalle>? Detalles { get; set; } = new();
    }
}