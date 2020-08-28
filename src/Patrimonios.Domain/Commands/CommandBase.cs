using prmToolkit.NotificationPattern;

namespace Patrimonios.Domain.Commands
{
    public abstract class CommandBase : Notifiable, ICommandBase
    {
        public abstract void Validate();
    }
}
