using MediatR;
using Patrimonios.Domain.Notifications;
using Patrimonios.Domain.Repositories.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.EventHandlers
{
    public class PatrimonioLogEventHandler : INotificationHandler<PatrimonioNotification>
    {
        public PatrimonioLogEventHandler(IPatrimonioLogEventRepository repository)
        {
            _repository = repository;
        }

        private readonly IPatrimonioLogEventRepository _repository;

        public Task Handle(PatrimonioNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _repository.Add(notification);
            });
        }
    }
}
