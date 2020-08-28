using prmToolkit.NotificationPattern;

namespace Patrimonios.Domain.Commands
{
    public abstract class CommandBase : Notifiable
    {
        public abstract void Validate();
    }
}
