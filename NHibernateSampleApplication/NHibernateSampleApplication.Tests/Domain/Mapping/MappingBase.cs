using NHibernate;
using NHibernateSampleApplication.Repository.Configuration;
using NUnit.Framework;

namespace NHibernateSampleApplication.Tests.Domain.Mapping
{
    public class MappingBase
    {
        protected ISession Session { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            ISessionFactoryBuilder sessionFactoryBuilder = new SessionFactoryBuilder(new SqlLiteConfigurationBuilder());
            ISessionFactory sessionFactory = sessionFactoryBuilder.Build();
            Session = sessionFactory.OpenSession();
        }

        [TearDown]
        public void AfterEachTest()
        {
            Session.Close();
            Session.Dispose();
        }
    }
}