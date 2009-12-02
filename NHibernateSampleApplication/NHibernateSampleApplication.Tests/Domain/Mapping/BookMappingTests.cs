using System.Collections.Generic;
using FluentNHibernate.Testing;
using NHibernateSampleApplication.Domain;
using NUnit.Framework;

namespace NHibernateSampleApplication.Tests.Domain.Mapping
{
    [TestFixture]
    public class BookMappingTests : MappingBase
    {
        [Test]
        public void Should_properly_map_book()
        {
            Book book = new Book
                            {
                                ISBN = "2345",
                                Title = "Test Title",
                                Price = 34.89m
                            };

            List<Author> authors = new List<Author>
                                       {
                                           new Author
                                               {
                                                   Name = "Test Author",
                                               }
                                       };

            new PersistenceSpecification<Book>(Session)
                .CheckProperty(p => p.ISBN, book.ISBN)
                .CheckProperty(p => p.Title, book.Title)
                .CheckProperty(p => p.Price, book.Price)
                .CheckList(p => p.Authors, authors)
                .VerifyTheMappings();
        }
    }
}