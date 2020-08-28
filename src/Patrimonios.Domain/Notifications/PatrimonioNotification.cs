using MediatR;
using System;

namespace Patrimonios.Domain.Notifications
{
    public class PatrimonioNotification : INotification
    {
        public PatrimonioNotification()
        {
            EventDate = DateTimeOffset.UtcNow;
        }

        public PatrimonioNotification(string @event, Guid id, string nome, Guid marcaId, string descricao, string numeroDoTombo)
        {
            Event = @event;
            Id = id;
            Nome = nome;
            MarcaId = marcaId;
            Descricao = descricao;
            NumeroDoTombo = numeroDoTombo;
            EventDate = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }
        public string Descricao { get; set; }
        public string NumeroDoTombo { get; set; }
        public string Event { get; set; }
        public DateTimeOffset EventDate { get; set; }
    }
}
