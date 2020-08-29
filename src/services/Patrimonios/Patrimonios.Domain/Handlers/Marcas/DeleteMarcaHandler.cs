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
    public class DeleteMarcaHandler : Notifiable,
        IRequestHandler<DeleteMarcaCommand, CommandResult<DeleteMarcaCommandResult>>
    {
        public DeleteMarcaHandler(
            IMarcaRepository marcaRepository,
            IPatrimonioRepository patrimonioRepository,
            IMediator mediator)
        {
            _marcaRepository = marcaRepository;
            _patrimonioRepository = patrimonioRepository;
            _mediator = mediator;
        }

        private readonly IMarcaRepository _marcaRepository;
        private readonly IPatrimonioRepository _patrimonioRepository;
        private readonly IMediator _mediator;

        public async Task<CommandResult<DeleteMarcaCommandResult>> Handle(DeleteMarcaCommand command, CancellationToken cancellationToken)
        {
            command.Validate();
            if (command.IsInvalid())
                return await Task.FromResult(ErrorCommandResult<DeleteMarcaCommandResult>.Create(command.Notifications));

            var marca = _marcaRepository.GetById(command.Id);

            if (marca == null)
                AddNotification(nameof(Marca.Id), Message.X0_NAO_ENCONTRADO.ToFormat(string.Concat("'", command.Id, "'")));

            if (IsInvalid())
                return await Task.FromResult(ErrorCommandResult<DeleteMarcaCommandResult>.Create(Notifications));

            var patrimonios = _patrimonioRepository.GetAllFromMarcaId(marca.Id);
            foreach (var p in patrimonios)
            {
                _patrimonioRepository.Delete(p.Id);

                if (_mediator != null)
                    await _mediator.Publish(new PatrimonioNotification("deleted", p.Id, p.Nome, p.MarcaId, p.Descricao, p.NumeroDoTombo), cancellationToken);
            }

            _marcaRepository.Delete(marca.Id);

            if (_mediator != null)
                await _mediator.Publish(new MarcaNotification("deleted", marca.Id, marca.Nome), cancellationToken);

            return await Task.FromResult(SuccessCommandResult<DeleteMarcaCommandResult>.Create((DeleteMarcaCommandResult)marca));
        }
    }
}
