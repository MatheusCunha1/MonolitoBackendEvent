using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Entidade;
using MonolitoBackend.Infra.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonolitoBackend.Core.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly ApplicationDbContext _context;

        public ParticipantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Participante>> GetAllParticipantsAsync()
        {
            return await _context.Participantes.ToListAsync();
        }

        public async Task<Participante> GetParticipantByIdAsync(int id)
        {
            return await _context.Participantes.FindAsync(id);
        }

        public async Task<Participante> CreateParticipantAsync(Participante participante)
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();
            return participante;
        }

        public async Task<Participante> UpdateParticipantAsync(Participante participante)
        {
            _context.Participantes.Update(participante);
            await _context.SaveChangesAsync();
            return participante;
        }

        public async Task<bool> DeleteParticipantAsync(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null) return false;

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Participante> AddParticipantToEventAsync(int eventoId, Participante participante)
        {
            var evento = await _context.Eventos.FindAsync(eventoId);
            if (evento == null)
            {
                return null;
            }

            participante.EventoId = eventoId;
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            return participante;
        }
    }
}
