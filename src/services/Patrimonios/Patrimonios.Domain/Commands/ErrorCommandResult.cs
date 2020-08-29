using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;

namespace Patrimonios.Domain.Commands
{
    public class ErrorCommandResult<T> : CommandResult<T> where T : class
    {
        public IEnumerable<Notification> Errors { get; set; }

        public static ErrorCommandResult<T> Create(IEnumerable<Notification> errors)
        {
            return new ErrorCommandResult<T>
            {
                Errors = errors
            };
        }

    }
}
