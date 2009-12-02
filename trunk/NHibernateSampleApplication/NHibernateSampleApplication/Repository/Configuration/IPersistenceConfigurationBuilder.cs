using FluentNHibernate.Cfg.Db;

namespace NHibernateSampleApplication.Repository.Configuration
{
    public interface IPersistenceConfigurationBuilder
    {
        IPersistenceConfigurer Build();
    }
}