using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Marcas;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Notifications;
using Patrimonios.Domain.Repositories;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.Handlers.Marcas
{
    public class CreateMarcaHandler : Notifiable,
        IRequestHandler<CreateMarcaCommand, CommandResult<CreateMarcaCommandResult>>
    {
        public CreateMarcaHandler(
            IMarcaRepository marcaRepository,
            IMediator mediator)
        {
            _marcaRepository = marcaRepository;
            _mediator = mediator;
        }

        private readonly IMarcaRepository _marcaRepository;
        private readonly IMediator _mediator;

        public async Task<CommandResult<CreateMarcaCommandResult>> Handle(CreateMarcaCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.IsInvalid())
                return await Task.FromResult(ErrorCommandResult<CreateMarcaCommandResult>.Create(command.Notifications));

            var marca = new Marca(command.Nome);

            if (_marcaRepository.GetByName(marca.Nome) != null)
                AddNotification(nameof(Marca.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<CreateMarcaCommandResult>.Create(Notifications));

            _marcaRepository.Add(marca);

            if (_mediator != null)
                await _mediator.Publish(new MarcaNotification("created", marca.Id, marca.Nome), cancellationToken);

            return await Task.FromResult(SuccessCommandResult<CreateMarcaCommandResult>.Create((CreateMarcaCommandResult)marca));
        }
    }
}
