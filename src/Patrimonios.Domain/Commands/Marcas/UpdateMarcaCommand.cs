﻿using MediatR;
using Patrimonios.Domain.Resources;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Patrimonios.Domain.Commands.Marcas
{
    public class UpdateMarcaCommand : CommandBase, IRequest<CommandResult<UpdateMarcaCommandResult>>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public override void Validate()
        {
            if (Id == Guid.Empty)
                AddNotification(nameof(Id), Message.X0_INVALIDO.ToFormat(string.Concat("'", Id, "'")));

            new AddNotifications<UpdateMarcaCommand>(this)
                .IfNullOrInvalidLength(x => x.Nome, 3, 100, Message.X0_EH_OBIGATORIO_E_DEVE_TER_ENTRE_X1_E_X2_CARACTERES.ToFormat(nameof(Nome), 3, 100));
        }
    }
}
