using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Patrimonios.Domain.Commands.Patrimonios
{
    public class DeletePatrimonioCommand : CommandBase, IRequest<CommandResult<DeletePatrimonioCommandResult>>
    {
        [Required]
        public Guid Id { get; set; }

        public override void Validate() { }
    }
}
