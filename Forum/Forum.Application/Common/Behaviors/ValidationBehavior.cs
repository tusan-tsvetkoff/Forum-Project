using ErrorOr;
using Forum.Application.Authentication.Commands.Register;
using Forum.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Behaviors
{
    internal class ValidateRegisterCommandBehavior :
        IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(
            RegisterCommand request,
            RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
            CancellationToken cancellationToken)
        {
            var result = await next();
            return result;
        }
    }
}
