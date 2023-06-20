﻿using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.PostAggregate.Events;
using Forum.Data.PostAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Posts.Events;

public class PostCreatedEventHandler : INotificationHandler<PostCreated>
{
    private readonly IAuthorRepository _authorRepository;

    public PostCreatedEventHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task Handle(PostCreated postCreatedEvent, CancellationToken cancellationToken)
    {
       // Gotta add some way to check the actual existance of the author.. fuck
       if(await _authorRepository.GetByAuthorIdAsync(postCreatedEvent.Post.AuthorId) is not Author author)
        {
            throw new InvalidOperationException($"Post has invalid author id (author id: {postCreatedEvent.Post.AuthorId}, post id: {postCreatedEvent.Post.Id}");
        }

        author.AddPostId((PostId)postCreatedEvent.Post.Id);

        await _authorRepository.UpdateAsync(author);
    }
}
