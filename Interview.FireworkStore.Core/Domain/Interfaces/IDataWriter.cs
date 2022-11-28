using System.Collections.Generic;

namespace Interview.FireworkStore.Core.Domain.Interfaces
{
    public interface IDataWriter<T>
    {
        bool Create(T order);
        bool Create(IEnumerable<T> objs);
    }
}
