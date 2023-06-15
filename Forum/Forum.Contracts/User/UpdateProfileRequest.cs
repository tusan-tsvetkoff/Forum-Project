using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.User;

public record UpdateProfileRequest(
    string? FirstName,
    string? LastName,
    string? Username,
    string? Password,
    string? About);
