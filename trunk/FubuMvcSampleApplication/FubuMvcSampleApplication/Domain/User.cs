using System;

namespace FubuMvcSampleApplication.Domain
{
    public class User : IEquatable<User>
    {
        public string UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public bool Equals(User user)
        {
            if (ReferenceEquals(null, user)) return false;
            if (ReferenceEquals(this, user)) return true;
            return Equals(user.UserId, UserId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (User)) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (UserId != null ? UserId.GetHashCode() : 0);
                return result;
            }
        }
    }
}
