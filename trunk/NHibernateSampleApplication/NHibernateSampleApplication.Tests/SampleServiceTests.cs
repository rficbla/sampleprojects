using System.Linq;
using FluentAssert;
using NHibernateSampleApplication.Domain;
using NHibernateSampleApplication.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace NHibernateSampleApplication.Tests
{
    public class SampleServiceTests
    {
        [TestFixture]
        public class When_asked_to_update_the_book
        {
            private IRepository _repository;
            private ISampleService sampleService;
            private Book _expectedBook;
            private Author _expectedAuthor;
            private Book _actualBook;

            [SetUp]
            public void BeforeEachTest()
            {
                _repository = MockRepository.GenerateMock<IRepository>();
                sampleService = new SampleService(_repository);

                _expectedBook = new Book
                                    {
                                        Title = "Book",
                                        Price = 10m
                                    };

                _expectedAuthor = new Author
                                      {
                                          Name = "Author"
                                      };

                _repository.Expect(x => x.Save(_expectedBook)).Repeat.Once();

                _actualBook = sampleService.UpdateBook(_expectedBook, _expectedAuthor);
            }

            [TearDown]
            public void AfterEachTest()
            {
                _repository.VerifyAllExpectations();
            }

            [Test]
            public void Should_update_the_price()
            {
                _actualBook.ShouldNotBeNull();
                _actualBook.Price.ShouldBeEqualTo(SampleService.price);
            }

            [Test]
            public void Should_add_the_author()
            {
                _actualBook.ShouldNotBeNull();
                _actualBook.Authors.First().ShouldBeEqualTo(_expectedAuthor);
            }
        }
    }
}