using Forum.Data.PostAggregate.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Posts.Events;

public class DummyHandler : INotificationHandler<PostCreated>
{
    public Task Handle(PostCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
