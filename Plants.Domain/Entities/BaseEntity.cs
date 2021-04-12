using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plants.Domain.Entities
{
    [DataContract(IsReference = true)]
    public abstract class BaseEntity
    {
    }
}
