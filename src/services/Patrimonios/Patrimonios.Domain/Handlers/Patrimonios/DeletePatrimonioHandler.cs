using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using Patrimonios.Domain.Entities;
using Patrimonios.Domain.Notifications;
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
        public DeletePatrimonioHandler(
            IPatrimonioRepository patrimonioRepository,
            IMediator mediator)
        {
            _patrimonioRepository = patrimonioRepository;
            _mediator = mediator;
        }

        private readonly IPatrimonioRepository _patrimonioRepository;
        private readonly IMediator _mediator;

        public async Task<CommandResult<DeletePatrimonioCommandResult>> Handle(DeletePatrimonioCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.IsInvalid())
                return await Task.FromResult(ErrorCommandResult<DeletePatrimonioCommandResult>.Create(command.Notifications));

            Patrimonio patrimonio = _patrimonioRepository.GetById(command.Id);

            if (patrimonio == null)
                AddNotification(nameof(Patrimonio.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<DeletePatrimonioCommandResult>.Create(Notifications));

            _patrimonioRepository.Delete(patrimonio.Id);

            if (_mediator != null)
                await _mediator.Publish(new PatrimonioNotification("deleted", patrimonio.Id, patrimonio.Nome, patrimonio.MarcaId, patrimonio.Descricao, patrimonio.NumeroDoTombo), cancellationToken);

            return await Task.FromResult(SuccessCommandResult<DeletePatrimonioCommandResult>.Create((DeletePatrimonioCommandResult)patrimonio));
        }
    }
}
