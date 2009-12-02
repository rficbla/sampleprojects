using FluentNHibernate.Mapping;

namespace NHibernateSampleApplication.Domain.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable();
            HasManyToMany(x => x.Books)
                .Inverse()
                .Cascade.All();
        }
    }
}