using ErrorOr;
using Forum.Contracts.Post;
using Forum.Data.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Posts.Queries.GetPost;

 public record GetPostQuery(
     Guid PostId) : IRequest<ErrorOr<PostResponse>>;