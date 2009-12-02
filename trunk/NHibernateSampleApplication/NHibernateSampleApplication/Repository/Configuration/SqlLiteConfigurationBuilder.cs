using FluentNHibernate.Cfg.Db;

namespace NHibernateSampleApplication.Repository.Configuration
{
    public class SqlLiteConfigurationBuilder : IPersistenceConfigurationBuilder
    {
        public IPersistenceConfigurer Build()
        {
            return SQLiteConfiguration
                .Standard
                .InMemory()
                .ConnectionString(connectionString =>
                                  connectionString.Is(
                                      "Data Source=:memory:;New=True;Pooling=True;Max Pool Size=1;"))
                .ShowSql();
        }
    }
}