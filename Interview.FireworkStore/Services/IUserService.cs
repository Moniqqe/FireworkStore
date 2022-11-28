using System.Collections.Generic;
using Interview.FireworkStore.Core.Domain.Entity;

namespace Interview.FireworkStore.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        IEnumerable<User> GetByName(string name);
    }
}