using Forum.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Entities
{
    public class Post
    {
       public Post(
       Guid id,
       string title,
       string content,
       Guid userId,
       DateTime lastEditedDate,
       DateTime createdDate)
        {
            Id = id;
            Title = title;
            Content = content;
            UserId = userId;
            LastEditedDate = lastEditedDate;
            CreatedDate = createdDate;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Content { get; }
        public Guid UserId { get; }
        public User User { get; }
        //  public List<Comment> Comments { get; }
        public DateTime CreatedDate { get; }
        public DateTime LastEditedDate { get; }
    }
}
