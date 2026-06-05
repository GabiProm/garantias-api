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
    }
}
