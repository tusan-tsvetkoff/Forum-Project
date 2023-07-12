using ErrorOr;
using Forum.Data.TagEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Tags.Commands;

public record CreateTagCommand(
    string Name) : IRequest<ErrorOr<Tag>>;
