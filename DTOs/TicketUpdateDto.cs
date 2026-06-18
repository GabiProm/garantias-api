namespace Garantias.API.DTOs
{
    public class TicketUpdateDto
    {
        public int Id {get; set; }
        public DateTime? FechaValidacion { get; set; }
        public DateTime? FechaGestionGarantia { get; set; }
        public string? Observacion { get; set; }

        public int? TipoDano { get; set; } // ✅ NUEVO
        public bool? ProcedeGarantia { get; set; } // ✅ NUEVO
        public DateTime? FechaReporte { get; set; } // ✅ NUEVO
        public string? TicketRimac { get; set; } // ✅ NUEVO
        public string? NroCaso { get; set; } // ✅ NUEVO
    }
}
