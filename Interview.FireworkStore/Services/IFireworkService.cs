using Interview.FireworkStore.Core.Domain.Entity;
using System.Collections.Generic;

namespace Interview.FireworkStore.Services
{
    public interface IFireworkService
    {
        IEnumerable<Firework> GetAll();

        Firework? GetById(int id);
    }
}
