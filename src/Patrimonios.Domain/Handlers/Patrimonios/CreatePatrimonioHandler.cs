using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Entities;
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
        public CreatePatrimonioHandler(IPatrimonioRepository patrimonioRepository)
        {
            _patrimonioRepository = patrimonioRepository;
        }

        private readonly IPatrimonioRepository _patrimonioRepository;

        public async Task<CommandResult<CreatePatrimonioCommandResult>> Handle(CreatePatrimonioCommand command, CancellationToken cancellationToken)
        {
            Patrimonio patrimonio = new Patrimonio(command.Nome, (Guid)command.MarcaId, command.Descricao);

            if (_patrimonioRepository.GetByName(patrimonio.Nome) != null)
                AddNotification(nameof(Patrimonio.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<CreatePatrimonioCommandResult>.Create(Notifications));

            _patrimonioRepository.Add(patrimonio);

            return await Task.FromResult(SuccessCommandResult<CreatePatrimonioCommandResult>.Create((CreatePatrimonioCommandResult)patrimonio));
        }
    }
}
