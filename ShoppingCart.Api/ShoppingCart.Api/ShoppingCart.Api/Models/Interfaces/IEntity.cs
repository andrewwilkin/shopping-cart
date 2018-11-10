using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
