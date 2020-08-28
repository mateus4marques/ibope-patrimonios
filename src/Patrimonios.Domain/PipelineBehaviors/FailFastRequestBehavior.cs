using MediatR;
using Patrimonios.Domain.Commands;
using Patrimonios.Domain.Commands.Patrimonios;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Patrimonios.Domain.PipelineBehaviors
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ICommandResult
    {

        public FailFastRequestBehavior() { }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICommandBase && request is INotifiable)
            {
                ((ICommandBase)request).Validate();

                if (((INotifiable)request).IsInvalid())
                {

                }
            }

            return next();
        }
    }
}

