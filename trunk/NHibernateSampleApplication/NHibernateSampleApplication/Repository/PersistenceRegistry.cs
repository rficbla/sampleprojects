using NHibernate;
using NHibernateSampleApplication.Repository.Configuration;
using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace NHibernateSampleApplication.Repository
{
    public class PersistenceRegistry : Registry
    {
        public PersistenceRegistry()
        {
            ForRequestedType<IPersistenceConfigurationBuilder>()
                .TheDefaultIsConcreteType<SqlLiteConfigurationBuilder>();

            ForRequestedType<ISessionFactory>().AsSingletons()
                .TheDefault.Is.ConstructedBy(x => x.GetInstance<ISessionFactoryBuilder>().Build());

            ForRequestedType<ISessionContainer>().CacheBy(InstanceScope.Hybrid)
                .TheDefaultIsConcreteType<SessionContainer>();
        }
    }
}