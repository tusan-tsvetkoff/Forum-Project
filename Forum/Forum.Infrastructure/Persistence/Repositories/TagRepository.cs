using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.TagEntity;
using Forum.Data.TagEntity.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Persistence.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ForumDbContext _dbContext;

    public TagRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Tag tag)
    {
        _dbContext.Add(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(string tagName)
    {
        return await _dbContext.Tags.AnyAsync(t => t.Name == tagName);
    }

    public async Task<Tag?> GetTagByIdAsync(TagId tagId)
    {
        return await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
    }

    public async Task<Tag?> GetTagByNameAsync(string? name)
    {
        return await _dbContext.Tags.FirstOrDefaultAsync(t => t.Name == name);
    }

    public async Task<List<Tag>> GetTagsAsync()
    {
        return await _dbContext.Tags.ToListAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _dbContext.Update(tag);
        await _dbContext.SaveChangesAsync();
    }

}
