using System;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using SampleMvc.Controllers;
using SampleMvc.Domain;
using SampleMvc.Models;
using SampleMvc.Repository;

namespace SampleMvc.Tests
{
    [TestFixture]
    public class When_you_navigate_to_the_UserDetails_page
    {
        private IUserRepository _userRepository;

        private UserController _userController;

        [SetUp]
        public void SetUp()
        {
            _userRepository = MockRepository.GenerateMock<IUserRepository>();

            _userController = new UserController(_userRepository);
        }

        [Test]
        public void Should_display_the_UserDetail_form_with_emptyfields()
        {
            ActionResult result = _userController.UserDetails();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof (ActionResult), result);
        }
    }

    [TestFixture]
    public class When_given_an_UserId
    {
        private IUserRepository _userRepository;

        private UserController _userController;

        [SetUp]
        public void SetUp()
        {
            _userRepository = MockRepository.GenerateMock<IUserRepository>();

            _userController = new UserController(_userRepository);
        }

        [Test]
        public void Should_return_an_action_result()
        {
            _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(new User());
            ActionResult result = _userController.PopulateDetails(new UserModel {UserId = "test"});

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof (JsonResult), result);
        }


        [Test]
        public void Should_return_corresponding_User_Details()
        {
            const string userId = "userId";
            User user = new User
                            {
                                LastName = "TestLastName",
                                FirstName = "TestFirstName"
                            };

            _userRepository.Expect(x => x.GetUser(userId)).IgnoreArguments().Return(user);

            UserModel userModel = new UserModel {UserId = "userId"};

            JsonResult result = _userController.PopulateDetails(userModel);

            Assert.IsNotNull(result);

            Assert.IsAssignableFrom(typeof (UserResultModel), result.Data);
            UserResultModel userResultModel = (UserResultModel) result.Data;

            Assert.AreEqual(user.LastName, userResultModel.LastName);
            Assert.AreEqual(user.FirstName, userResultModel.FirstName);
            Assert.IsEmpty(userResultModel.Message);
        }

        [Test]
        public void Should_return_an_error_message_if_the_UserId_is_empty()
        {
            JsonResult result = _userController.PopulateDetails(new UserModel());

            Assert.IsNotNull(result);

            Assert.IsAssignableFrom(typeof(UserResultModel), result.Data);
            UserResultModel userResultModel = (UserResultModel)result.Data;

            Assert.IsTrue(userResultModel.Message.Length > 0);
        }

        [Test]
        public void Should_return_an_error_message_if_the_UserId_is_not_found()
        {
            const string userid = "userId";

            _userRepository.Expect(x => x.GetUser(userid)).IgnoreArguments().Return(null);
            JsonResult result = _userController.PopulateDetails(new UserModel {UserId = userid});

            Assert.IsNotNull(result);

            Assert.IsAssignableFrom(typeof(UserResultModel), result.Data);
            UserResultModel userResultModel = (UserResultModel)result.Data;

            Assert.IsTrue(userResultModel.Message.Length > 0);
        }
    }
}