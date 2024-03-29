﻿using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Interfaces.Persistence;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);
    Task<bool> CommentExistsAsync(CommentId commentId);
    Task DeleteAsync(Comment comment);
    Task<Comment> GetByIdAsync(CommentId commentId);
    Task<IQueryable<Comment>> GetCommentsAsync();
    Task UpdateAsync(Comment comment, CancellationToken cancellationToken);
}
