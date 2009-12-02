using System;

using FubuMvcSampleApplication.Controllers;
using FubuMvcSampleApplication.Domain;

namespace FubuMvcSampleApplication.Persistence.Mapping
{
    public class UserMapper : IMapper<UserEditViewModel, User>
    {
        public User MapFrom(UserEditViewModel userEditEditViewModel)
        {
            return new User
                {
                    UserId = userEditEditViewModel.UserId,
                    LastName = userEditEditViewModel.LastName,
                    FirstName = userEditEditViewModel.FirstName,
                    DateOfBirth =
                        userEditEditViewModel.DateOfBirth != null
                            ? Convert.ToDateTime(userEditEditViewModel.DateOfBirth)
                            : (DateTime?)null
                };
        }
    }
}