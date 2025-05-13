using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.Entidade;
using MonolitoBackend.Core.Interfaces;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventosController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos() =>
            Ok(await _eventService.GetAllEventsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id) =>
            Ok(await _eventService.GetEventByIdAsync(id));

        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento([FromBody] Evento evento)
        {
            var created = await _eventService.CreateEventAsync(evento);
            return CreatedAtAction(nameof(GetEvento), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] Evento evento)
        {
            if (id != evento.Id) return BadRequest("ID do evento na URL não bate com o corpo da requisição.");
            await _eventService.UpdateEventAsync(evento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
