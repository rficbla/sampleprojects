using System;
using System.Collections.Generic;
using System.Linq;
using FubuMvcSampleApplication.Controllers;
using FubuMvcSampleApplication.Domain;
using FubuMvcSampleApplication.Persistence;
using FubuMvcSampleApplication.Persistence.Mapping;
using FubuMvcSampleApplication.Web.DisplayModels;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace FubuMvcSampleApplication.Tests.Controllers
{
    public class UserControllerTests
    {
        [TestFixture]
        public class When_you_ask_for_a_list_of_users
        {
            private RhinoAutoMocker<UserController> _mocker;
            private IUserRepository _userRepository;
            private UserController _userController;
            private User _user;
            private List<User> _users;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userRepository = _mocker.Get<IUserRepository>();
                _userController = _mocker.ClassUnderTest;

                _user = new User
                            {
                                UserId = "id",
                                LastName = "last",
                                FirstName = "first",
                                DateOfBirth = Convert.ToDateTime("1/1/1972")
                            };

                _users = new List<User>
                             {
                                 _user
                             };
            }

            [Test]
            public void Should_render_a_view_that_contains_a_list_of_all_the_users_in_the_system()
            {
                _userRepository.Expect(x => x.GetUsers()).Return(_users);

                UserListViewModel usersList = _userController.Index(new UserIndexViewModel());

                List<UserDisplayModel> userDisplayModels = usersList.Users.ToList();
                Assert.IsTrue(userDisplayModels.Count() > 0);

                Assert.AreEqual(_user.UserId, userDisplayModels[0].UserId);
                Assert.AreEqual(_user.LastName, userDisplayModels[0].LastName);
                Assert.AreEqual(_user.FirstName, userDisplayModels[0].FirstName);
            }
        }

        [TestFixture]
        public class When_asked_to_save
        {
            private RhinoAutoMocker<UserController> _mocker;
            private IUserRepository _userRepository;
            private UserController _userController;
            private UserEditViewModel _userEditModel;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userRepository = _mocker.Get<IUserRepository>();
                _userController = _mocker.ClassUnderTest;

                _userEditModel = new UserEditViewModel
                                     {
                                         UserId = "Id"
                                     };
            }

            [Test]
            public void Should_display_error_message_if_UserId_is_blank()
            {
                _userEditModel = new UserEditViewModel();
                _userRepository.Expect(x => x.Save(null)).IgnoreArguments();

                UserEditViewModel userDisplayModel = _userController.Save(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.AreEqual("UserId cannot be blank", userDisplayModel.Message);
            }

            [Test]
            public void Should_save_or_update_the_user_and_return_a_success_message()
            {
                _userRepository.Expect(x => x.Save(null)).IgnoreArguments();

                UserEditViewModel userDisplayModel = _userController.Save(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.AreEqual("User saved successfully", userDisplayModel.Message);
            }

            [Test]
            public void Should_set_the_error_message_if_an_exception_is_thrown()
            {
                _userRepository.Expect(x => x.Save(null)).IgnoreArguments().Throw(new Exception());

                UserEditViewModel userDisplayModel = _userController.Save(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.IsTrue(userDisplayModel.Message.Contains("User could not be saved:"));
            }
        }

        [TestFixture]
        public class When_asked_to_delete_a_user
        {
            private RhinoAutoMocker<UserController> _mocker;
            private IUserRepository _userRepository;
            private UserController _userController;
            private UserEditViewModel _userEditModel;
            private IMapper<UserEditViewModel, User> _userMapper;
            private User _user;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userRepository = _mocker.Get<IUserRepository>();
                _userMapper = _mocker.Get<IMapper<UserEditViewModel, User>>();
                _userController = _mocker.ClassUnderTest;

                const string userId = "Test";
                _userEditModel = new UserEditViewModel
                                     {
                                         UserId = userId
                                     };
                _user = new User
                            {
                                UserId = userId
                            };
            }

            [Test]
            public void Should_delete_the_user_and_display_a_success_message()
            {
                _userMapper.Expect(x => x.MapFrom(null)).IgnoreArguments().Return(_user);
                _userRepository.Expect(x => x.Delete(null)).IgnoreArguments();
                _userRepository.Expect(x => x.GetUsers()).Return(new List<User>());

                UserListViewModel userDisplayModel = _userController.Delete(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.AreEqual("User Test successfully deleted", userDisplayModel.Message);
            }

            [Test]
            public void Should_set_the_error_message_if_an_exception_is_thrown()
            {
                _userMapper.Expect(x => x.MapFrom(null)).IgnoreArguments().Return(_user);
                _userRepository.Expect(x => x.Delete(null)).IgnoreArguments().Throw(new ArgumentException());
                _userRepository.Expect(x => x.GetUsers()).Return(new List<User>());

                UserListViewModel userDisplayModel = _userController.Delete(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.IsTrue(userDisplayModel.Message.Contains("User Test could not be deleted:"));
            }
        }

        [TestFixture]
        public class When_you_click_on_cancel
        {
            private RhinoAutoMocker<UserController> _mocker;
            private UserController _userController;
            private UserEditViewModel _userEditModel;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userController = _mocker.ClassUnderTest;
                _userEditModel = new UserEditViewModel();
            }

            [Test]
            public void Should_redirect_to_Index_page()
            {
                UserIndexViewModel userDisplayModel = _userController.Cancel(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
            }
        }

        [TestFixture]
        public class When_asked_to_add_new_user
        {
            private RhinoAutoMocker<UserController> _mocker;
            private UserController _userController;
            private UserEditViewModel _userEditModel;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userController = _mocker.ClassUnderTest;
                _userEditModel = new UserEditViewModel();
            }

            [Test]
            public void Should_display_new_or_empty_object()
            {
                UserEditViewModel userDisplayModel = _userController.New(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.IsNull(userDisplayModel.UserId);
            }
        }

        [TestFixture]
        public class When_asked_to_edit_a_user
        {
            private RhinoAutoMocker<UserController> _mocker;
            private IUserRepository _userRepository;
            private UserController _userController;
            private User _user;
            private UserEditViewModel _userEditModel;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userRepository = _mocker.Get<IUserRepository>();
                _userController = _mocker.ClassUnderTest;
                _user = new User
                            {
                                UserId = "Id",
                                LastName = "LName",
                                FirstName = "FName"
                            };
                _userEditModel = new UserEditViewModel();
            }

            [Test]
            public void Should_display_new_or_empty_object_if_user_doesnot_exist()
            {
                _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(null);

                UserEditViewModel userDisplayModel = _userController.Edit(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.IsNull(userDisplayModel.UserId);
                Assert.IsNull(userDisplayModel.LastName);
                Assert.IsNull(userDisplayModel.FirstName);
                Assert.IsNull(userDisplayModel.DateOfBirth);
            }

            [Test]
            public void Should_display_the_details_if_user_exists()
            {
                _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(_user);

                UserEditViewModel userDisplayModel = _userController.Edit(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.AreEqual(_user.UserId, userDisplayModel.UserId);
                Assert.AreEqual(_user.LastName, userDisplayModel.LastName);
                Assert.AreEqual(_user.FirstName, userDisplayModel.FirstName);
                Assert.IsNull(userDisplayModel.DateOfBirth);
            }
        }

        [TestFixture]
        public class When_asked_to_display_user_details
        {
            private RhinoAutoMocker<UserController> _mocker;
            private IUserRepository _userRepository;
            private UserController _userController;
            private User _user;
            private UserEditViewModel _userEditModel;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<UserController>();
                _userRepository = _mocker.Get<IUserRepository>();
                _userController = _mocker.ClassUnderTest;
                _user = new User
                            {
                                UserId = "Id",
                                LastName = "LName",
                                FirstName = "FName"
                            };
                _userEditModel = new UserEditViewModel();
            }

            [Test]
            public void Should_display_empty_object_if_user_doesnot_exist()
            {
                _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(null);

                UserEditViewModel userDisplayModel = _userController.Edit(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.IsNull(userDisplayModel.UserId);
                Assert.IsNull(userDisplayModel.LastName);
                Assert.IsNull(userDisplayModel.FirstName);
                Assert.IsNull(userDisplayModel.DateOfBirth);
            }

            [Test]
            public void Should_display_user_details_if_the_user_exists()
            {
                _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(_user);

                UserEditViewModel userDisplayModel = _userController.Edit(_userEditModel);

                Assert.IsNotNull(userDisplayModel);
                Assert.AreEqual(_user.UserId, userDisplayModel.UserId);
                Assert.AreEqual(_user.LastName, userDisplayModel.LastName);
                Assert.AreEqual(_user.FirstName, userDisplayModel.FirstName);
                Assert.IsNull(userDisplayModel.DateOfBirth);
            }

            [Test]
            public void Should_set_the_message_to_View_Result_for_a_normal_request()
            {
                _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(_user);

                UserEditViewModel userDisplayModel = _userController.Display(_userEditModel);
                Assert.AreEqual("View Result", userDisplayModel.Message);
            }

            [Test]
            public void Should_set_the_message_to_Json_Result_for_a_json_request()
            {
                _userEditModel.HTTP_X_REQUESTED_WITH = "XMLHttpRequest";

                _userRepository.Expect(x => x.GetUser(null)).IgnoreArguments().Return(_user);

                UserEditViewModel userDisplayModel = _userController.Display(_userEditModel);
                Assert.AreEqual("Json Result", userDisplayModel.Message);
            }
        }
    }
}