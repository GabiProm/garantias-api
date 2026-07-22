using System.ComponentModel.DataAnnotations;
using Garantias.API.Models;

namespace Garantias.API.DTOs
{
    public class TicketCreateDto
    {
        [Required(ErrorMessage = "El número de inventario es obligatorio.")]
        public string? NroInventario { get; set; }
        [Required(ErrorMessage = "La serie es obligatoria.")]
        public string? Serie { get; set; }
        public string? TicketRimac { get; set; }
        public string? NroCaso { get; set; }
        [Required(ErrorMessage = "El problema es obligatorio.")]
        public string? Problema { get; set; }

        public bool ProcedeGarantia { get; set; }
        public TipoDanoEnum TipoDano { get; set; }
    }
}