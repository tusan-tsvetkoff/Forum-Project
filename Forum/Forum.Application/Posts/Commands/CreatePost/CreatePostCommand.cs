using ErrorOr;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Posts.Commands.CreatePost;

public record CreatePostCommand(
    string AuthorId,
    string Title,
    string Content) : IRequest<ErrorOr<Post>>;
