using Forum.Data.TagEntity;
using Forum.Data.TagEntity.ValueObjects;

namespace Forum.Application.Common.Interfaces.Persistence;

public interface ITagRepository
{
    Task AddAsync(Tag tag);
    Task<bool> Exists(string tagName);
    Task<Tag?> GetTagByIdAsync(TagId tagId);
    Task<Tag?> GetTagByNameAsync(string? name);
    Task UpdateAsync(Tag tag); // probably not needed
}
