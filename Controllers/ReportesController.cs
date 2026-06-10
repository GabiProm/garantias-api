using Microsoft.AspNetCore.Mvc;
using Garantias.API.Data;

namespace Garantias.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Estado de tickets
        [HttpGet("estado")]
        public IActionResult GetEstado()
        {
            var abiertos = _context.Tickets
                .Count(t => t.FechaGestionGarantia == null);

            var cerrados = _context.Tickets
                .Count(t => t.FechaGestionGarantia != null);

            return Ok(new
            {
                Abiertos = abiertos,
                Cerrados = cerrados
            });
        }

        // ✅ Tipo de daño
            [HttpGet("tipo-dano")]
        public IActionResult TipoDano()
        {
            var data = _context.Tickets
                .GroupBy(t => t.TipoDano)
                .Select(g => new
                {
                    TipoDano = g.Key.ToString(),
                    Total = g.Count()
                })
                .ToList();

            return Ok(data);
        }

        // ✅ Componentes más reportados
        [HttpGet("componentes")]
        public IActionResult Componentes()
        {
            var data = _context.TicketDetalles
                .GroupBy(d => d.Componente.Nombre)
                .Select(g => new
                {
                    Componente = g.Key,
                    Total = g.Count()
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            return Ok(data);
        }

        // ✅ Reporte mensual de tickets
        [HttpGet("mensual")]
        public IActionResult ReporteMensual()
        {
            var data = _context.Tickets
                .GroupBy(t => new { t.FechaReporte.Year, t.FechaReporte.Month })
                .Select(g => new
                {
                    Año = g.Key.Year,
                    Mes = g.Key.Month,
                    Total = g.Count()
                })
                .OrderBy(x => x.Año)
                .ThenBy(x => x.Mes)
                .ToList();

            return Ok(data);
        }

        // ✅ Procedencia de garantía
        [HttpGet("garantia")]
        public IActionResult Garantia()
        {
            var data = _context.Tickets
                .GroupBy(t => t.ProcedeGarantia)
                .Select(g => new
                {
                    ProcedeGarantia = g.Key ? "Sí" : "No",
                    Total = g.Count()
                })
                .ToList();

            return Ok(data);
        }

    }
}
