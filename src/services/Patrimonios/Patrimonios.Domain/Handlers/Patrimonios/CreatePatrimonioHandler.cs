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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.Handlers.Patrimonios
{
    public class CreatePatrimonioHandler : Notifiable,
        IRequestHandler<CreatePatrimonioCommand, CommandResult<CreatePatrimonioCommandResult>>
    {
        public CreatePatrimonioHandler(
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

        public async Task<CommandResult<CreatePatrimonioCommandResult>> Handle(CreatePatrimonioCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.IsInvalid())
                return await Task.FromResult(ErrorCommandResult<CreatePatrimonioCommandResult>.Create(command.Notifications));

            Patrimonio patrimonio = new Patrimonio(command.Nome, (Guid)command.MarcaId, command.Descricao);

            if (_patrimonioRepository.GetByName(patrimonio.Nome) != null)
                AddNotification(nameof(Patrimonio.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));


            if (_marcaRepository.GetById((Guid)command.MarcaId) == null)
                AddNotification(nameof(Patrimonio.MarcaId), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.MarcaId, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<CreatePatrimonioCommandResult>.Create(Notifications));

            _patrimonioRepository.Add(patrimonio);

            if (_mediator != null)
                await _mediator.Publish(new PatrimonioNotification("created", patrimonio.Id, patrimonio.Nome, patrimonio.MarcaId, patrimonio.Descricao, patrimonio.NumeroDoTombo), cancellationToken);

            return await Task.FromResult(SuccessCommandResult<CreatePatrimonioCommandResult>.Create((CreatePatrimonioCommandResult)patrimonio));
        }
    }
}
