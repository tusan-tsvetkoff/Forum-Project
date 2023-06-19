using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Models;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get;}

    public void ClearDomainEvents();  
}
