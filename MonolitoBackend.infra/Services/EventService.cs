using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Entidade;
using MonolitoBackend.Infra.Data;

namespace MonolitoBackend.Core.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<Evento>> GetAllEventsAsync()
        {
            return await _context.Eventos
                .Include(e => e.Participantes)
                .ToListAsync();
        }

      
        public async Task<Evento> GetEventByIdAsync(int id)
        {
            return await _context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Evento> CreateEventAsync(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<Evento> UpdateEventAsync(Evento evento)
        {
            _context.Eventos.Update(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
