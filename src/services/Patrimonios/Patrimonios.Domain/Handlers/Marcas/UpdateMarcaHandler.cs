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
    public class UpdateMarcaHandler : Notifiable,
        IRequestHandler<UpdateMarcaCommand, CommandResult<UpdateMarcaCommandResult>>
    {
        public UpdateMarcaHandler(
            IMarcaRepository marcaRepository,
            IMediator mediator)
        {
            _marcaRepository = marcaRepository;
            _mediator = mediator;
        }

        private readonly IMarcaRepository _marcaRepository;
        private readonly IMediator _mediator;

        public async Task<CommandResult<UpdateMarcaCommandResult>> Handle(UpdateMarcaCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.IsInvalid())
                return await Task.FromResult(ErrorCommandResult<UpdateMarcaCommandResult>.Create(command.Notifications));

            var marca = _marcaRepository.GetById(command.Id);

            if (marca == null)
                AddNotification(nameof(Marca.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (_marcaRepository.GetByName(command.Nome) != null)
                AddNotification(nameof(Marca.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<UpdateMarcaCommandResult>.Create(Notifications));

            marca.Update(command.Nome);

            _marcaRepository.Update(marca);

            if (_mediator != null)
                await _mediator.Publish(new MarcaNotification("updated", marca.Id, marca.Nome), cancellationToken);

            return await Task.FromResult(SuccessCommandResult<UpdateMarcaCommandResult>.Create((UpdateMarcaCommandResult)marca));
        }
    }
}
