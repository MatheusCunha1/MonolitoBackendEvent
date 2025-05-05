using System;
using System.Collections.Generic;

namespace MonolitoBackend.Core.Entidade
{
    public class Evento
    {
        public int Id { get; set; }

        public string Titulo { get; set; }  // Campo agora opcional

        public string Localizacao { get; set; }  // Campo agora opcional

        public DateTime Data { get; set; }  // Ainda obrigatório

        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();
    }
}
