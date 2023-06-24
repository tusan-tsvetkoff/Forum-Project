using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.CommentAggregate.Events;
public record CommentCreated(Comment Comment) : IDomainEvent;
