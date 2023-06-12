using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.User;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Country);
