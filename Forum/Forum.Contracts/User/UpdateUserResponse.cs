using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.User;

public record UpdateUserResponse
{
    public string Message { get; init; } = "User updated successfully";
}
