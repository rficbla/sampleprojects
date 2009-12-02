using System;

using FubuMvcSampleApplication.Controllers;
using FubuMvcSampleApplication.Domain;
using FubuMvcSampleApplication.Persistence.Mapping;

using NUnit.Framework;

namespace FubuMvcSampleApplication.Tests.Persistence.Mapping
{
    public class UserMapperTests
    {
        [TestFixture]
        public class When_asked_for_User
        {
            private IMapper<UserEditViewModel, User> _mapper;
            private User _user;
            private UserEditViewModel _userEditModel;

            [SetUp]
            public void SetUp()
            {
                _user = new User
                    {
                        UserId = "Id",
                        LastName = "Last",
                        FirstName = "First",
                    };
                _mapper = new UserMapper();
            }

            [Test]
            public void Should_map_UserAddEditViewModel_to_User()
            {
                _user.DateOfBirth = Convert.ToDateTime("12/12/1990");
                _userEditModel = new UserEditViewModel(_user);

                User actualUser = _mapper.MapFrom(_userEditModel);
                Assert.AreEqual(_user.UserId, actualUser.UserId);
                Assert.AreEqual(_user.LastName, actualUser.LastName);
                Assert.AreEqual(_user.FirstName, actualUser.FirstName);
                Assert.AreEqual(_user.DateOfBirth, actualUser.DateOfBirth);
            }

            [Test]
            public void Should_map_UserAddEditViewModel_to_User_with_DateOfBirth_set_to_null()
            {
                _userEditModel = new UserEditViewModel(_user);

                User actualUser = _mapper.MapFrom(_userEditModel);
                Assert.AreEqual(_user.UserId, actualUser.UserId);
                Assert.AreEqual(_user.LastName, actualUser.LastName);
                Assert.AreEqual(_user.FirstName, actualUser.FirstName);
                Assert.IsNull(actualUser.DateOfBirth);
            }
        }
    }
}