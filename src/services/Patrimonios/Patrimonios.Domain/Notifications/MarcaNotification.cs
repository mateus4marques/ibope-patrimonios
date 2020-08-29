using MediatR;
using System;

namespace Patrimonios.Domain.Notifications
{
    public class MarcaNotification : INotification
    {
        public MarcaNotification()
        {
            EventDate = DateTimeOffset.UtcNow;
        }

        public MarcaNotification(string @event, Guid id, string nome)
        {
            Event = @event;
            Id = id;
            Nome = nome;
            EventDate = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Event { get; set; }
        public DateTimeOffset EventDate { get; set; }
    }
}
