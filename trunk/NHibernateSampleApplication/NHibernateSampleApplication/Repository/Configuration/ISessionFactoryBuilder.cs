using NHibernate;

namespace NHibernateSampleApplication.Repository.Configuration
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory Build();
    }
}