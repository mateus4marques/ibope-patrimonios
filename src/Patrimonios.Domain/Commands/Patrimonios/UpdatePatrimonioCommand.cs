using MediatR;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Patrimonios.Domain.Commands.Patrimonios
{
    public class UpdatePatrimonioCommand : CommandBase, IRequest<CommandResult<UpdatePatrimonioCommandResult>>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public Guid? MarcaId { get; set; }

        public string Descricao { get; set; }

        public override void Validate()
        {
            if (Id == Guid.Empty)
                AddNotification(nameof(Id), Message.X0_INVALIDO.ToFormat(string.Concat("'", Id, "'")));

            if (MarcaId == Guid.Empty)
                AddNotification(nameof(MarcaId), Message.X0_INVALIDO.ToFormat(string.Concat("'", MarcaId, "'")));

            new AddNotifications<UpdatePatrimonioCommand>(this)
                 .IfNullOrInvalidLength(x => x.Nome, 3, 100, Message.X0_EH_OBIGATORIO_E_DEVE_TER_ENTRE_X1_E_X2_CARACTERES.ToFormat(nameof(Nome), 3, 100));

            if (!string.IsNullOrEmpty(Descricao))
                new AddNotifications<UpdatePatrimonioCommand>(this)
                    .IfNullOrInvalidLength(x => x.Descricao, 0, 300, Message.X0_DEVE_TER_ATE_X1_CARACTERES.ToFormat(nameof(Descricao), 300));
        }
    }
}
