using Microsoft.AspNetCore.Mvc;
using Garantias.API.Data;
using Garantias.API.Models;

namespace Garantias.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComponentesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Componentes.ToList());
        }

        [HttpPost]
        public IActionResult Create(Componente componente)
        {
            var existe = _context.Componentes
                .Any(c => c.Nombre.ToLower() == componente.Nombre.ToLower());

            if (existe)
                return BadRequest("El componente ya existe");

            _context.Componentes.Add(componente);
            _context.SaveChanges();

            return Ok(componente);
        }
    }
}