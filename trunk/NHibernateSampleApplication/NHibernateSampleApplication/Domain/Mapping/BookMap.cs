using FluentNHibernate.Mapping;

namespace NHibernateSampleApplication.Domain.Mapping
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(x => x.Id);
            Map(x => x.ISBN).Not.Nullable();
            Map(x => x.Title);
            HasManyToMany(x => x.Authors)
                .Cascade.All();
            Map(x => x.Price);
        }
    }
}