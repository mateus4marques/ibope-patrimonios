using MediatR;
using Patrimonios.Domain.Notifications;
using Patrimonios.Domain.Repositories.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.EventHandlers
{
    public class MarcaLogEventHandler : INotificationHandler<MarcaNotification>
    {
        public MarcaLogEventHandler(IMarcaLogEventRepository repository)
        {
            _repository = repository;
        }

        private readonly IMarcaLogEventRepository _repository;

        public Task Handle(MarcaNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _repository.Add(notification);
            });
        }
    }
}
