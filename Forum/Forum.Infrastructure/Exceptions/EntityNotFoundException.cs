﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Exceptions
{
    public class EntityNotFoundException:ApplicationException
    {
        public EntityNotFoundException(string message):base(message)
        {
        }
    }
}
