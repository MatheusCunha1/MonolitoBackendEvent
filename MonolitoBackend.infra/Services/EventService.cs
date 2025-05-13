using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Entidade;
using MonolitoBackend.Infra.Data;
using System.ComponentModel.DataAnnotations;

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
            var evento = await _context.Eventos
                .Include(e => e.Participantes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
                throw new Exception("Evento não encontrado.");

            return evento;
        }

        public async Task<Evento> CreateEventAsync(Evento evento)
        {
            var existe = await _context.Eventos
                .AnyAsync(e => e.Titulo == evento.Titulo && e.Data.Date == evento.Data.Date);

            if (existe)
                throw new ValidationException("Já existe um evento com esse título nessa data.");

            if (evento.Data.Date < DateTime.Today)
                throw new ValidationException("Não é possível criar um evento no passado.");

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<Evento> UpdateEventAsync(Evento evento)
        {
            var existente = await _context.Eventos.FindAsync(evento.Id);
            if (existente == null)
                throw new Exception("Evento não encontrado para atualização.");

            if (evento.Data.Date < DateTime.Today)
                throw new ValidationException("Data inválida: o evento não pode ser no passado.");

            existente.Titulo = evento.Titulo;
            existente.Localizacao = evento.Localizacao;
            existente.Data = evento.Data;

            _context.Eventos.Update(existente);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                throw new Exception("Evento não encontrado para exclusão.");

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
