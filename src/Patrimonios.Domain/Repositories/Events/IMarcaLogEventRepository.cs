using Patrimonios.Domain.Notifications;

namespace Patrimonios.Domain.Repositories.Events
{
    public interface IMarcaLogEventRepository
    {
        void Add(MarcaNotification marcaNotification);
    }
}
