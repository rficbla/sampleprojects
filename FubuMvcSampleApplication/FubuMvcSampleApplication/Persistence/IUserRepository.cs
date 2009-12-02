using System.Collections.Generic;

using FubuMvcSampleApplication.Domain;

namespace FubuMvcSampleApplication.Persistence
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(string userId);
        void Save(User user);
        void Delete(User user);
    }
}