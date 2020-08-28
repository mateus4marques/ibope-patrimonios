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
    public class DeleteMarcaHandler : Notifiable,
        IRequestHandler<DeleteMarcaCommand, CommandResult<DeleteMarcaCommandResult>>
    {
        public DeleteMarcaHandler(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        private readonly IMarcaRepository _marcaRepository;

        public async Task<CommandResult<DeleteMarcaCommandResult>> Handle(DeleteMarcaCommand command, CancellationToken cancellationToken)
        {
            var marca = _marcaRepository.GetById(command.Id);

            if (marca == null)
                AddNotification(nameof(Marca.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<DeleteMarcaCommandResult>.Create(Notifications));
            
            _marcaRepository.Delete(marca.Id);

            return await Task.FromResult(SuccessCommandResult<DeleteMarcaCommandResult>.Create((DeleteMarcaCommandResult)marca));
        }
    }
}
