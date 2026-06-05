using Microsoft.EntityFrameworkCore;
using Garantias.API.Models;

namespace Garantias.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketDetalle> TicketDetalles { get; set; }
        public DbSet<Componente> Componentes { get; set; }  
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}