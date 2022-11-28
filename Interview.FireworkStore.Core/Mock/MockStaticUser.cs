using Interview.FireworkStore.Core.Domain.Entity;

namespace Interview.FireworkStore.Core.Mock
{
    public static class MockStaticUser
    {
        public static User GetStaticTestUser()
        {
            return new User
            {
                Id = 1,
                UserName = "John123"
            };
        }
    }
}
