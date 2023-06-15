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
        public DateTime CreatedDate { get; private set; }
        public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();
        public IReadOnlyList<CommentId> CommentIds => _commentIds.AsReadOnly();

        // Properties for profile page/update/
        public string About { get; private set; }

        public User(
            string firstName,
            string lastName,
            string email,
            string username,
            string password,
            DateTime createdDate,
            UserId? userId = null)
            : base(id: userId ?? UserId.CreateUnique())
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
            CreatedDate = createdDate;
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
                password: password,
                createdDate: DateTime.UtcNow);
        }

        public void UpdateProfile(
            string firstName,
            string lastName,
            string username,
            string password,
            string about)
        {
            if(!string.IsNullOrWhiteSpace(firstName))
            {
                FirstName = firstName;
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                LastName = lastName;
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                Username = username;
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                Password = password;
            }

            if (!string.IsNullOrWhiteSpace(about))
            {
                About = about;
            }
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
