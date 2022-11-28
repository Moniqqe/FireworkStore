using Interview.FireworkStore.Core.Domain.Entity;
using System.Collections.Generic;

namespace Interview.FireworkStore.Core.Domain.Interfaces
{
    public interface IDataReader
    {
        IEnumerable<Firework> LoadFireworks();

        IEnumerable<Order> LoadOrders();
    }
}
