using FluentNHibernate.Testing;
using NHibernateSampleApplication.Domain;
using NUnit.Framework;

namespace NHibernateSampleApplication.Tests.Domain.Mapping
{
    [TestFixture]
    public class AuthorMappingTests : MappingBase
    {
        [Test]
        public void Should_properly_map_author()
        {
            Author author = new Author
                                {
                                    Name = "Test Author"
                                };


            new PersistenceSpecification<Author>(Session)
                .CheckProperty(p => p.Name, author.Name)
                .VerifyTheMappings();
        }
    }
}