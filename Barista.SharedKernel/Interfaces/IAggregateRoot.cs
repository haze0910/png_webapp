using System;
using System.Collections.Generic;
using System.Text;

namespace Barista.SharedKernel.Interfaces
{
    public interface IAggregateRoot
    {
        Guid Id { get; }

    }
}
