using System.Collections.Generic;
using System.Linq;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Mock;

namespace Interview.FireworkStore.Services
{
    public class UserService : IUserService
    {
        private readonly IEnumerable<User> _users;

        public UserService()
        {
            _users = new[]
            {
                MockStaticUser.GetStaticTestUser()
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public IEnumerable<User> GetByName(string name)
        {
            return _users.Where(user => user.UserName.Equals(name));
        }
    }
}
