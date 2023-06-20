using ErrorOr;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Users.Commands.Delete;

public record DeleteUserCommand(
    Guid UserId) : IRequest<ErrorOr<Deleted>>;

