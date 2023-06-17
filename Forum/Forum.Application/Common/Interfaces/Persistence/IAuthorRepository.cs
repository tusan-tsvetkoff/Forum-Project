using Forum.Data.AuthorAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Interfaces.Persistence;

public interface IAuthorRepository
{
    public Author Add(Author author);
    public Author? GetByAuthorId(string authorId);
    public Author? GetByUserId(Guid userId);
}
