using Forum.Contracts.Common;

namespace Forum.Contracts.User;


public record UserResponse(
    string Id,
    string Username,
    string FirstName,
    string LastName,
    int PostCount,
    int CommentCount);

public record UserResponseList(
    List<UserResponse> Users,
       PageInfo PageInfo);
