using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.UserAggregate
{
    public sealed class User : AggregateRoot<UserId, Guid>
    {
        private readonly List<PostId> _postIds = new();
        private readonly List<CommentId> _commentIds = new();
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; } // Need to hash
        public string Country { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();
        public IReadOnlyList<CommentId> CommentIds => _commentIds.AsReadOnly();

        public User(
            string firstName,
            string lastName,
            string email,
            string username,
            string password,
            UserId? userId = null)
            : base(id: userId ?? UserId.CreateUnique())
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
        }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string username,
            string password
            )
        {
            return new User(
                firstName: firstName,
                lastName: lastName,
                email: email,
                username: username,
                password: password);
        }

        public void UpdateCountry(string country)
        {
            Country = country;
        }

        public void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }
    }
}
