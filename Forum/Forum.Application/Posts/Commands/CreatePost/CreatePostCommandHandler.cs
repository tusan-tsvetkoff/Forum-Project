﻿using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using Forum.Data.Common.Errors;

namespace Forum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;

    public CreatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<Post>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
         // Get the author profile of the user
         var userToAuthor = _userRepository.GetUserByIdAsync(UserId.Create(command.UserId));
         
        // Check if the user exists (still not sure if this should be done here or in the controller)
        /*if (!userToAuthor.IsCompletedSuccessfully)
        {
            return Errors.User.NotFound;
        }*/

        // Get the author profile of the user
        var author = _authorRepository.GetByUserIdAsync(UserId.Create(userToAuthor.Result!.Id.Value));
        // Shouldn't have to check author existance here, since it should be created when the user is created
        await Console.Out.WriteLineAsync($"AuthorId: {author.Result.Id.Value}");

        // Create the post with the author's ID
        var post = Post.Create(
            command.Content,
            command.Title,
            AuthorId.Create(author.Result.Id.Value));

        await _postRepository.AddAsync(post);

        return post;
    }
}
