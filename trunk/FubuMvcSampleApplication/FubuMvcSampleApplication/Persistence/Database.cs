using System;
using System.Collections.Generic;

using FubuMvcSampleApplication.Domain;

namespace FubuMvcSampleApplication.Persistence
{
    public class Database
    {
        public static List<User> Users = new List<User>
            {
                new User
                    {
                        UserId = "john_doe",
                        LastName = "Doe",
                        FirstName = "John",
                        DateOfBirth = Convert.ToDateTime("12/12/1980")
                    },
                new User
                    {
                        UserId = "user2",
                        LastName = "SomeLastName",
                        FirstName = "SomeFirstName",
                        DateOfBirth = Convert.ToDateTime("1/1/1972")
                    },
            };
    }
}