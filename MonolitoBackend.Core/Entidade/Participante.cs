using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MonolitoBackend.Core.Entidade
{
    public class Participante
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int EventoId { get; set; }

        [JsonIgnore] // <- ESSENCIAL PARA EVITAR O CICLO
        public Evento? Evento { get; set; }
    }
}
