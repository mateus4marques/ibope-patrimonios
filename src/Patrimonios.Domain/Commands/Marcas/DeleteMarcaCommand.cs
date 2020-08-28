using MediatR;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Patrimonios.Domain.Commands.Marcas
{
    public class DeleteMarcaCommand : CommandBase, IRequest<CommandResult<DeleteMarcaCommandResult>>
    {
        [Required]
        public Guid Id { get; set; }

        public override void Validate()
        {
            if (Id == Guid.Empty)
                AddNotification(nameof(Id), Message.X0_INVALIDO.ToFormat(string.Concat("'", Id, "'")));
        }
    }
}
