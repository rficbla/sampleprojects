using System;
using System.Collections.Generic;
using System.Linq;

using FubuMvcSampleApplication.Domain;
using FubuMvcSampleApplication.Persistence;

using NUnit.Framework;

namespace FubuMvcSampleApplication.Tests.Persistence
{
    public class UserRepositoryTests
    {
        [TestFixture]
        public class When_asked_to_retrieve_all_users
        {
            public IUserRepository _userRepository;
            private User _user;
            private string _userId;

            [SetUp]
            public void SetUp()
            {
                _userRepository = new UserRepository();

                _userId = "TestId";
                _user = new User
                    {
                        UserId = _userId,
                        LastName = "Lname",
                        FirstName = "Fname"
                    };
                _userRepository.Save(_user);
            }

            [Test]
            public void Should_get_an_ienumerable_list_of_all_the_users_from_the_database()
            {
                List<User> actualUsers = _userRepository.GetUsers().ToList();
                Assert.IsTrue(actualUsers.Count > 0);
            }
        }

        [TestFixture]
        public class When_asked_to_retrieve_a_user
        {
            public IUserRepository _userRepository;
            private User _user;
            private string _userId;

            [SetUp]
            public void SetUp()
            {
                _userRepository = new UserRepository();

                _userId = "TestId";
                _user = new User
                    {
                        UserId = _userId,
                        LastName = "Lname",
                        FirstName = "Fname"
                    };
                _userRepository.Save(_user);
            }

            [Test]
            public void Should_get_the_user_with_the_matching_userid_from_the_database()
            {
                User actualUser = _userRepository.GetUser(_userId);
                Assert.IsNotNull(actualUser);
                Assert.AreEqual(_userId, actualUser.UserId);
            }
        }

        [TestFixture]
        public class When_asked_to_save_a_user
        {
            public IUserRepository _userRepository;
            private User _user;
            private string _userId;

            [SetUp]
            public void SetUp()
            {
                _userRepository = new UserRepository();

                _userId = "TestId";
                _user = new User
                    {
                        UserId = _userId,
                        LastName = "Lname",
                        FirstName = "Fname"
                    };
            }

            [Test]
            public void Should_add_the_user_if_it_already_does_not_exist_in_the_database()
            {
                _userRepository.Save(_user);
                Assert.IsTrue(Database.Users.Exists(user => user.UserId == _userId));
            }

            [Test]
            public void Should_update_the_user_if_it_already_doesnot_exist_in_the_database()
            {
                _user.LastName = "L";
                _user.FirstName = "F";
                _userRepository.Save(_user);
                User actualUser = Database.Users.Find(user => user.UserId == _userId);
                Assert.IsNotNull(actualUser);
                Assert.AreEqual("L", actualUser.LastName);
                Assert.AreEqual("F", actualUser.FirstName);
            }
        }

        [TestFixture]
        public class When_asked_to_delete_a_user
        {
            public IUserRepository _userRepository;
            private User _user;
            private string _userId;

            [SetUp]
            public void SetUp()
            {
                _userRepository = new UserRepository();

                _userId = "TestId";
                _user = new User
                    {
                        UserId = _userId,
                    };
                _userRepository.Save(_user);
            }

            [Test]
            public void Should_delete_user_from_the_database()
            {
                _userRepository.Delete(_user);
                Assert.IsFalse(Database.Users.Exists(user => user.UserId == _userId));
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_throw_an_exception_if_you_attempt_to_delete_reserve_users_from_the_database()
            {
                _user.UserId = "john_doe";
                _userRepository.Delete(_user);
            }
        }
    }
}