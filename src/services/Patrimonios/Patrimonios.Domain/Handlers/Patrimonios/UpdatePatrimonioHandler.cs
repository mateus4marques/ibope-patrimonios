using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Notifications;
using Patrimonios.Domain.Repositories;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.Handlers.Patrimonios
{
    public class UpdatePatrimonioHandler : Notifiable,
        IRequestHandler<UpdatePatrimonioCommand, CommandResult<UpdatePatrimonioCommandResult>>
    {
        public UpdatePatrimonioHandler(
            IPatrimonioRepository patrimonioRepository,
            IMarcaRepository marcaRepository,
            IMediator mediator)
        {
            _patrimonioRepository = patrimonioRepository;
            _marcaRepository = marcaRepository;
            _mediator = mediator;
        }

        private readonly IPatrimonioRepository _patrimonioRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IMediator _mediator;

        public async Task<CommandResult<UpdatePatrimonioCommandResult>> Handle(UpdatePatrimonioCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.IsInvalid())
                return await Task.FromResult(ErrorCommandResult<UpdatePatrimonioCommandResult>.Create(command.Notifications));

            Patrimonio patrimonio = _patrimonioRepository.GetById(command.Id);

            if (patrimonio == null)
                AddNotification(nameof(Patrimonio.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (_patrimonioRepository.GetByName(command.Nome) != null)
                AddNotification(nameof(Patrimonio.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));

            if (_marcaRepository.GetById((Guid)command.MarcaId) == null)
                AddNotification(nameof(Patrimonio.MarcaId), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.MarcaId, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<UpdatePatrimonioCommandResult>.Create(Notifications));

            patrimonio.Update(command.Nome, (Guid)command.MarcaId, command.Descricao);

            _patrimonioRepository.Update(patrimonio);

            if (_mediator != null)
                await _mediator.Publish(new PatrimonioNotification("updated", patrimonio.Id, patrimonio.Nome, patrimonio.MarcaId, patrimonio.Descricao, patrimonio.NumeroDoTombo), cancellationToken);

            return await Task.FromResult(SuccessCommandResult<UpdatePatrimonioCommandResult>.Create((UpdatePatrimonioCommandResult)patrimonio));
        }
    }
}
