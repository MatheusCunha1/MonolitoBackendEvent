using MonolitoBackend.Core.Entidade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonolitoBackend.Core.Interfaces
{
    public interface IEventService
    {
        Task<List<Evento>> GetAllEventsAsync();
        Task<Evento> GetEventByIdAsync(int id);
        Task<Evento> CreateEventAsync(Evento evento);
        Task<Evento> UpdateEventAsync(Evento evento);
        Task<bool> DeleteEventAsync(int id);
    }
}
