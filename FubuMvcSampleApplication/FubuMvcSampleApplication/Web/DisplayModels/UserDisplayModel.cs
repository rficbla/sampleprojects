using FubuMvcSampleApplication.Domain;

namespace FubuMvcSampleApplication.Web.DisplayModels
{
    public class UserDisplayModel
    {
        public UserDisplayModel(User user)
        {
            UserId = user.UserId;
            LastName = user.LastName;
            FirstName = user.FirstName;
        }

        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}