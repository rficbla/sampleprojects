using System.Collections.Generic;
using System.Linq;
using SampleMvc.Domain;

namespace SampleMvc.Repository
{
    public class UserRepository : IUserRepository
    {
        #region IUserRepository Members

        public User GetUser(string userId)
        {
            return Database.Users.Where(user => user.UserId == userId).FirstOrDefault();
        }

        #endregion
    }

    public class Database
    {
        public static List<User> Users = new List<User>
                                             {
                                                 new User
                                                     {
                                                         UserId = "john_doe",
                                                         LastName = "Doe",
                                                         FirstName = "John",
                                                         Age = 25
                                                     },
                                                 new User
                                                     {
                                                         UserId = "user2",
                                                         LastName = "SomeLastName",
                                                         FirstName = "SomeFirstNAme",
                                                         Age = 28
                                                     },
                                             };
    }
}