using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Marcas;
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

namespace Patrimonios.Domain.Handlers.Marcas
{
    public class CreateMarcaHandler : Notifiable,
        IRequestHandler<CreateMarcaCommand, CommandResult<CreateMarcaCommandResult>>
    {
        public CreateMarcaHandler(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        private readonly IMarcaRepository _marcaRepository;

        public async Task<CommandResult<CreateMarcaCommandResult>> Handle(CreateMarcaCommand command, CancellationToken cancellationToken)
        {
            var marca = new Marca(command.Nome);

            if (_marcaRepository.GetByName(marca.Nome) != null)
                AddNotification(nameof(Marca.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<CreateMarcaCommandResult>.Create(Notifications));

            _marcaRepository.Add(marca);

            return await Task.FromResult(SuccessCommandResult<CreateMarcaCommandResult>.Create((CreateMarcaCommandResult)marca));
        }
    }
}
