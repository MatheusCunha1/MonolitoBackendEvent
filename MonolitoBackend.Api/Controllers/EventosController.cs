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
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            var eventos = await _eventService.GetAllEventsAsync();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var evento = await _eventService.GetEventByIdAsync(id);
            if (evento == null) return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento evento)
        {
            var created = await _eventService.CreateEventAsync(evento);
            return CreatedAtAction(nameof(GetEvento), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento evento)
        {
            if (id != evento.Id) return BadRequest();

            await _eventService.UpdateEventAsync(evento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var deleted = await _eventService.DeleteEventAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
