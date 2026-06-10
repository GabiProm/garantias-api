using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garantias.API.Data;
using Garantias.API.Models;
using Garantias.API.DTOs;
using Garantias.API.Helpers;

namespace Garantias.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET TODOS
        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _context.Tickets
                .Include(t => t.Detalles)
                .ThenInclude(d => d.Componente)
                .ToList();

            var result = tickets.Select(t => new
            {
                t.Id,
                t.NroInventario,
                t.Serie,
                t.TicketRimac,
                t.NroCaso,
                t.Problema,
                t.FechaReporte,
                t.FechaValidacion,
                t.ProcedeGarantia,
                TipoDano = TipoDanoHelper.GetDescripcion(t.TipoDano),
                t.FechaGestionGarantia,
                t.Observacion,
                Estado = t.FechaGestionGarantia == null ? "Abierto" : "Cerrado",

                Detalles = t.Detalles.Select(d => new
                {
                    d.Id,
                    Componente = d.Componente.Nombre,
                    d.TipoGarantia,
                    d.Observaciones,
                    d.FechaRegistro
                })
            });

            return Ok(result);
        }

        // ✅ BUSCAR FLEXIBLE (🔥 PRO)
        [HttpGet("buscar")]
        public IActionResult Buscar(string? serie, string? nroInventario)
        {
            if (string.IsNullOrEmpty(serie) && string.IsNullOrEmpty(nroInventario))
                return BadRequest("Debe enviar serie o nroInventario");

            var ticket = _context.Tickets
                .Include(t => t.Detalles)
                .ThenInclude(d => d.Componente)
                .FirstOrDefault(t =>
                    (!string.IsNullOrEmpty(serie) && t.Serie == serie) ||
                    (!string.IsNullOrEmpty(nroInventario) && t.NroInventario == nroInventario)
                );

            if (ticket == null)
                return NotFound("Ticket no encontrado");

            return Ok(new
            {
                ticket.Id,
                ticket.NroInventario,
                ticket.Serie,
                ticket.Problema,
                ticket.FechaReporte,
                ticket.FechaValidacion,
                ticket.ProcedeGarantia,
                TipoDano = TipoDanoHelper.GetDescripcion(ticket.TipoDano),
                ticket.FechaGestionGarantia,
                ticket.Observacion,
                Estado = ticket.FechaGestionGarantia == null ? "Abierto" : "Cerrado",

                Detalles = ticket.Detalles.Select(d => new
                {
                    d.Id,
                    Componente = d.Componente.Nombre,
                    d.TipoGarantia,
                    d.Observaciones
                })
            });
        }

        // ✅ CREAR
        [HttpPost]
        public IActionResult Create(TicketCreateDto dto)
        {
            var ticket = new Ticket
            {
                NroInventario = dto.NroInventario,
                Serie = dto.Serie,
                TicketRimac = dto.TicketRimac,
                NroCaso = dto.NroCaso,
                Problema = dto.Problema,
                TipoDano = dto.TipoDano,
                ProcedeGarantia = dto.ProcedeGarantia,
                FechaReporte = DateTime.Now
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return Ok(ticket);
        }

        // ✅ ACTUALIZAR FLEXIBLE (🔥 PRO)
        [HttpPut("buscar")]
        public IActionResult UpdateFlexible(
            string? serie,
            string? nroInventario,
            TicketUpdateDto dto)
        {
            if (string.IsNullOrEmpty(serie) && string.IsNullOrEmpty(nroInventario))
                return BadRequest("Debe enviar serie o nroInventario");

            var ticket = _context.Tickets.FirstOrDefault(t =>
                (!string.IsNullOrEmpty(serie) && t.Serie == serie) ||
                (!string.IsNullOrEmpty(nroInventario) && t.NroInventario == nroInventario)
            );

            if (ticket == null)
                return NotFound("Ticket no encontrado");

            // 🔴 NO modificar si cerrado
            if (ticket.FechaGestionGarantia != null)
                return BadRequest("El ticket ya está cerrado");

            // ✅ Fecha validación (siempre editable)
            if (dto.FechaValidacion.HasValue)
                ticket.FechaValidacion = dto.FechaValidacion;

            // ✅ Observación
            if (!string.IsNullOrEmpty(dto.Observacion))
                ticket.Observacion = dto.Observacion;

            // ✅ Cierre del ticket
            if (dto.FechaGestionGarantia.HasValue)
            {
                if (string.IsNullOrEmpty(dto.Observacion))
                    return BadRequest("Debe ingresar observación para cerrar el ticket");

                ticket.FechaGestionGarantia = dto.FechaGestionGarantia;
            }

            _context.SaveChanges();

            return Ok(new
            {
                ticket.Id,
                ticket.FechaValidacion,
                ticket.FechaGestionGarantia,
                ticket.Observacion,
                Estado = ticket.FechaGestionGarantia == null ? "Abierto" : "Cerrado"
            });
        }

        // ✅ AGREGAR COMPONENTE
        [HttpPost("{id}/agregar-componente")]
        public IActionResult AddDetalle(int id, TicketDetalleCreateDto dto)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
                return NotFound("Ticket no encontrado");

            if (!ticket.ProcedeGarantia)
                return BadRequest("No procede garantía");

            if (ticket.FechaGestionGarantia != null)
                return BadRequest("Ticket cerrado");

            var detalle = new TicketDetalle
            {
                TicketId = id,
                ComponenteId = dto.ComponenteId,
                TipoGarantia = dto.TipoGarantia,
                Observaciones = dto.Observaciones,
                FechaRegistro = DateTime.Now
            };

            _context.TicketDetalles.Add(detalle);
            _context.SaveChanges();

            var componente = _context.Componentes.Find(dto.ComponenteId);

            return Ok(new
            {
                detalle.Id,
                detalle.TicketId,
                Componente = componente?.Nombre,
                detalle.TipoGarantia,
                detalle.Observaciones,
                detalle.FechaRegistro
            });
        }

        // ✅ OBTENER POR ID (con detalles)
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _context.Tickets
                .Include(t => t.Detalles)
                .ThenInclude(d => d.Componente)
                .FirstOrDefault(t => t.Id == id);

            if (ticket == null)
                return NotFound();

            return Ok(new
            {
                ticket.Id,
                ticket.NroInventario,
                ticket.Serie,
                ticket.Problema,
                ticket.FechaReporte,
                ticket.FechaValidacion,
                ticket.ProcedeGarantia,
                TipoDano = TipoDanoHelper.GetDescripcion(ticket.TipoDano),
                ticket.FechaGestionGarantia,
                ticket.Observacion,
                Estado = ticket.FechaGestionGarantia == null ? "Abierto" : "Cerrado",

                Detalles = ticket.Detalles.Select(d => new
                {
                    d.Id,
                    Componente = d.Componente.Nombre,
                    d.TipoGarantia,
                    d.Observaciones,
                    d.FechaRegistro
                })
            });
        }

        // ✅ ELIMINAR
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _context.Tickets
                .Include(t => t.Detalles)
                .FirstOrDefault(t => t.Id == id);

            if (ticket == null)
                return NotFound();

            _context.TicketDetalles.RemoveRange(ticket.Detalles);
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return Ok("Eliminado correctamente");
        }
    }
}