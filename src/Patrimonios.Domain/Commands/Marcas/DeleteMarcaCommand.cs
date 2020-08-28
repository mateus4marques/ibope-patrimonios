using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Patrimonios.Domain.Commands.Marcas
{
    public class DeleteMarcaCommand : CommandBase, IRequest<CommandResult<DeleteMarcaCommandResult>>
    {
        [Required]
        public Guid Id { get; set; }

        public override void Validate() { }
    }
}
