using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Controller.Config;
using FubuMVC.Core.Controller.Results;
using FubuMvcSampleApplication.Domain;
using FubuMvcSampleApplication.Persistence;
using FubuMvcSampleApplication.Persistence.Mapping;
using FubuMvcSampleApplication.Web;
using FubuMvcSampleApplication.Web.DisplayModels;

namespace FubuMvcSampleApplication.Controllers
{
    public class UserController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper<UserEditViewModel, User> _mapper;
        private readonly IUrlResolver _urlResolver;
        private readonly IResultOverride _resultOverride;

        public UserController(IUserRepository userRepository, IMapper<UserEditViewModel, User> mapper,
                              IUrlResolver urlResolver, IResultOverride resultOverride)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _urlResolver = urlResolver;
            _resultOverride = resultOverride;
        }

        public UserListViewModel Index(UserIndexViewModel userIndexViewModel)
        {
            return GetUsers();
        }

        public UserEditViewModel New(UserEditViewModel userEditViewModel)
        {
            return new UserEditViewModel();
        }

        public UserEditViewModel Edit(UserEditViewModel userEditViewModel)
        {
            User user = _userRepository.GetUser(userEditViewModel.UserId);
            return new UserEditViewModel(user);
        }

        public UserEditViewModel Save(UserEditViewModel userEditViewModel)
        {
            if (String.IsNullOrEmpty(userEditViewModel.UserId))
            {
                userEditViewModel.Message = String.Format("UserId cannot be blank");
                return userEditViewModel;
            }
            User user = _mapper.MapFrom(userEditViewModel);
            try
            {
                _userRepository.Save(user);
                userEditViewModel.Message = "User saved successfully";
            }
            catch (Exception exception)
            {
                userEditViewModel.Message = String.Format("User could not be saved: {0}", exception.Message);
            }
            return userEditViewModel;
        }

        public UserListViewModel Delete(UserEditViewModel userEditViewModel)
        {
            User user = _mapper.MapFrom(userEditViewModel);
            string message;
            try
            {
                _userRepository.Delete(user);
                message = String.Format("User {0} successfully deleted", user.UserId);
            }
            catch (ArgumentException exception)
            {
                message = String.Format("User {0} could not be deleted: {1}", user.UserId, exception.Message);
            }
            UserListViewModel usersListViewModel = GetUsers();
            usersListViewModel.Message = message;
            return usersListViewModel;
        }

        public UserIndexViewModel Cancel(UserEditViewModel userEditViewModel)
        {
            _resultOverride.RedirectTo(_urlResolver.UrlFor<UserController>());
            return new UserIndexViewModel();
        }

        public UserEditViewModel Display(UserEditViewModel userEditViewModel)
        {
            User user = _userRepository.GetUser(userEditViewModel.UserId);
            return new UserEditViewModel(user)
                       {
                           Message = userEditViewModel.IsAjaxRequest() ? "Json Result" : "View Result"
                       };
        }

        private UserListViewModel GetUsers()
        {
            return new UserListViewModel
                       {
                           Users = _userRepository.GetUsers().Select(user => new UserDisplayModel(user)),
                       };
        }
    }

    [Serializable]
    public class UserIndexViewModel : ViewModel
    {
    }

    [Serializable]
    public class UserListViewModel : ViewModel
    {
        public IEnumerable<UserDisplayModel> Users { get; set; }
    }

    [Serializable]
    public class UserEditViewModel : ViewModel, ICanDetectAjaxRequests
    {
        public UserEditViewModel()
        {
        }

        public UserEditViewModel(User user)
        {
            if (user == null)
            {
                return;
            }
            UserId = user.UserId;
            LastName = user.LastName;
            FirstName = user.FirstName;
            DateOfBirth = user.DateOfBirth != null ? user.DateOfBirth.Value.ToShortDateString() : null;
        }

        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
        public string HTTP_X_REQUESTED_WITH { get; set; }
    }
}