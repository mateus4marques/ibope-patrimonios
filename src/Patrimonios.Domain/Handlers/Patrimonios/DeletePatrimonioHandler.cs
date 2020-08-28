using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Repositories;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.Handlers.Patrimonios
{
    public class DeletePatrimonioHandler : Notifiable,
        IRequestHandler<DeletePatrimonioCommand, CommandResult<DeletePatrimonioCommandResult>>
    {
        public DeletePatrimonioHandler(IPatrimonioRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        private readonly IPatrimonioRepository _marcaRepository;

        public async Task<CommandResult<DeletePatrimonioCommandResult>> Handle(DeletePatrimonioCommand command, CancellationToken cancellationToken)
        {
            Patrimonio marca = _marcaRepository.GetById(command.Id);

            if (marca == null)
                AddNotification(nameof(Patrimonio.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<DeletePatrimonioCommandResult>.Create(Notifications));

            _marcaRepository.Delete(marca.Id);

            return await Task.FromResult(SuccessCommandResult<DeletePatrimonioCommandResult>.Create((DeletePatrimonioCommandResult)marca));
        }
    }
}
