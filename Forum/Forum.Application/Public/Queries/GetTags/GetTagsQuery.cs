using ErrorOr;
using Forum.Contracts.Common;
using Forum.Data.TagEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Public.Queries.GetTags;

public record GetTagsQuery() : IRequest<ErrorOr<List<Tag>>>;
