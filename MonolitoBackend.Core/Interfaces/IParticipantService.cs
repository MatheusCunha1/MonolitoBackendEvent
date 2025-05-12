using MonolitoBackend.Core.Entidade;

namespace MonolitoBackend.Core.Interfaces
{
    public interface IParticipantService
    {
        Task<List<Participante>> GetAllParticipantsAsync();
        Task<Participante> GetParticipantByIdAsync(int id);
        Task<Participante> CreateParticipantAsync(Participante participante);
        Task<Participante> UpdateParticipantAsync(Participante participante);
        Task<bool> DeleteParticipantAsync(int id);
        Task<Participante> AddParticipantToEventAsync(int eventoId, Participante participante);
    }
}
