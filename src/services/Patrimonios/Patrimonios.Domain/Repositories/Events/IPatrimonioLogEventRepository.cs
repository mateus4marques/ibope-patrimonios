using Patrimonios.Domain.Notifications;

namespace Patrimonios.Domain.Repositories.Events
{
    public interface IPatrimonioLogEventRepository
    {
        void Add(PatrimonioNotification patrimonioNotification);
    }
}
