using NHibernateSampleApplication.Behavior;
using NHibernateSampleApplication.Domain;
using NUnit.Framework;
using StructureMap;
using FluentAssert;

namespace NHibernateSampleApplication.Tests
{
    [TestFixture, Explicit]
    public class SystemTest
    {
        private ISampleService sampleService;
        private IRequestHandler _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            BootStrapper.BootStrap();
            _unitOfWork = ObjectFactory.GetInstance<IRequestHandler>();
            sampleService = ObjectFactory.GetInstance<ISampleService>();
        }

        [Test]
        public void Should_save_the_book()
        {
            Book book = Program.GetBook();
            Author author = Program.GetAuthor();
            Book result = _unitOfWork.Invoke<Book, Author, Book>(book, author, sampleService.UpdateBook);
            result.Title.ShouldBeEqualTo(book.Title);
        }
    }
}