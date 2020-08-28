using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Marcas;
using Patrimonios.Domain.Entities;
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
        public UpdateMarcaHandler(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        private readonly IMarcaRepository _marcaRepository;

        public async Task<CommandResult<UpdateMarcaCommandResult>> Handle(UpdateMarcaCommand command, CancellationToken cancellationToken)
        {
            var marca = _marcaRepository.GetById(command.Id);

            if (marca == null)
                AddNotification(nameof(Marca.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (_marcaRepository.GetByName(command.Nome) != null)
                AddNotification(nameof(Marca.Nome), Message.X0_JA_EXISTE.ToFormat(string.Concat("'", command.Nome, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<UpdateMarcaCommandResult>.Create(Notifications));

            marca.Update(command.Nome);

            _marcaRepository.Update(marca);

            return await Task.FromResult(SuccessCommandResult<UpdateMarcaCommandResult>.Create((UpdateMarcaCommandResult)marca));
        }
    }
}
