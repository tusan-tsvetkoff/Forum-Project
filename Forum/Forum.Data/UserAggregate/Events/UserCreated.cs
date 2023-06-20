using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.UserAggregate.Events;

public record UserCreated(User User) : IDomainEvent;
