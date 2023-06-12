using Forum.Data.Comment.ValueObjects;
using Forum.Data.Commenter.ValueObjects;
using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Comment
{
    public sealed class Comment : AggregateRoot<CommentId>
    {
        public CommentId CommentId { get; set; }
        public string PostId { get; } 
        public string Content { get; }
        public CommenterId CommenterId { get;  }
        public DateTime CreatedDate { get; }
        public DateTime EditedDate { get; }

        public Comment(
            CommentId commentId,
            string content,
            string postId,
            CommenterId commenterId,
            DateTime createdDate,
            DateTime editedDate)
            : base(commentId)
        {
            Content = content;
            PostId = postId;
            CommenterId = commenterId;
            CreatedDate = createdDate;
            EditedDate = editedDate;
        }

        public static Comment Create(
            string content,
            string postId,
            CommenterId commenterId)
        {
            return new Comment(
                CommentId.CreateUnique(),
                content,
                postId,
                commenterId,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }
    }
}
