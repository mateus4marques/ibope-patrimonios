using MediatR;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Patrimonios.Domain.Commands.Marcas
{
    [Display(Name = "Marca")]
    public class CreateMarcaCommand : CommandBase, IRequest<CommandResult<CreateMarcaCommandResult>>
    {
        [Required]
        public string Nome { get; set; }

        public override void Validate()
        {
            new AddNotifications<CreateMarcaCommand>(this)
                .IfNullOrInvalidLength(x => x.Nome, 3, 100, Message.X0_EH_OBIGATORIO_E_DEVE_TER_ENTRE_X1_E_X2_CARACTERES.ToFormat(nameof(Nome), 3, 100));
        }
    }
}
