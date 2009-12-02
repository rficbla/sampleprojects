using SampleMvc.Domain;

namespace SampleMvc.Repository
{
    public interface IUserRepository
    {
        User GetUser(string userId);
    }
}