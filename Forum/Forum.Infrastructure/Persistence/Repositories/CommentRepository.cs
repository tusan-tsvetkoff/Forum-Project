using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ForumDbContext _dbContext;

    public CommentRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Comment comment)
    {
        await _dbContext.AddAsync(comment);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        _dbContext.Remove(comment);

        await _dbContext.SaveChangesAsync();
    }
}
