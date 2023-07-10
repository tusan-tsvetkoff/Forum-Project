using ErrorOr;
using Forum.Data.AuthorAggregate;
using Forum.Data.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Users.Queries.GetUser;

public record GetAuthorQuery(
    string AuthorId) : IRequest<ErrorOr<Author>>;
