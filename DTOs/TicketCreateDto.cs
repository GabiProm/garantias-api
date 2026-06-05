using Garantias.API.Models;

namespace Garantias.API.DTOs
{
    public class TicketCreateDto
    {
        public string? NroInventario { get; set; }
        public string? Serie { get; set; }
        public string? TicketRimac { get; set; }
        public string? NroCaso { get; set; }
        public string? Problema { get; set; }

        public bool ProcedeGarantia { get; set; }
        public TipoDanoEnum TipoDano { get; set; }
    }
}