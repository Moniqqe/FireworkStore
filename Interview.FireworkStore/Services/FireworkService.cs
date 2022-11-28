using System.Collections.Generic;
using System.Linq;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Domain.Interfaces;

namespace Interview.FireworkStore.Services
{
    public class FireworkService : IFireworkService
    {
        private readonly IEnumerable<Firework> _fireworks;

        public FireworkService(IDataReader dataReader)
        {
            _fireworks = dataReader.LoadFireworks();
        }

        public IEnumerable<Firework> GetAll()
        {
            return _fireworks;
        }

        public Firework? GetById(int id)
        {
            return _fireworks.FirstOrDefault(firework => firework.Id == id);
        }
    }
}
