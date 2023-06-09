using System;

namespace Forum.Models.Entities
{
    public class Post
    {
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Content {get; set;}
        public User UserId {get; set;}
    }
}
