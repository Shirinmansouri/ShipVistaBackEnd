using System;
using System.Collections.Generic;
using System.Text;

namespace Plants.Domain.Entities
{
    interface IEntity<T>
    {
        T Id { get; set; }
    }
}
