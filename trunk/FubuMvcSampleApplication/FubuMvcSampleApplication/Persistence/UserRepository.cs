using System;
using System.Collections.Generic;
using System.Linq;

using FubuMvcSampleApplication.Domain;

namespace FubuMvcSampleApplication.Persistence
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers()
        {
            return Database.Users;
        }

        public User GetUser(string userId)
        {
            return Database.Users.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public void Save(User user)
        {
            if (Database.Users.Contains(user))
            {
                Database.Users.Remove(user);
            }
            Database.Users.Add(user);
        }

        public void Delete(User user)
        {
            // code to demonstrate a case where if you cannot delete a user, that would prevent the redirect and a message is displayed
            if (user.UserId == "john_doe" || user.UserId == "user2")
            {
                throw new ArgumentException(String.Format("{0} was not deleted as a demonstration", user.UserId));
            }
            Database.Users.Remove(user);
        }
    }
}