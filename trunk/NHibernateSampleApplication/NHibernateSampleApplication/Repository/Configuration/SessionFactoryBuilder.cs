using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateSampleApplication.Domain.Mapping;

namespace NHibernateSampleApplication.Repository.Configuration
{
    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        private readonly IPersistenceConfigurationBuilder _persistenceConfigurationBuilder;
        private NHibernate.Cfg.Configuration _configuration;

        public SessionFactoryBuilder(IPersistenceConfigurationBuilder persistenceConfigurationBuilder)
        {
            _persistenceConfigurationBuilder = persistenceConfigurationBuilder;
        }

        public ISessionFactory Build()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(_persistenceConfigurationBuilder.Build())
                .Mappings(mapping => mapping.FluentMappings.AddFromAssemblyOf<BookMap>())
                .ExposeConfiguration(cfg => _configuration = cfg)
                .BuildSessionFactory();

            BuildSchema();

            return sessionFactory;
        }

        private void BuildSchema()
        {
            new SchemaExport(_configuration).Create(false, true);
        }
    }
}