using NHibernateSampleApplication.Domain;
using NHibernateSampleApplication.Repository;

namespace NHibernateSampleApplication
{
    public interface ISampleService
    {
        Book UpdateBook(Book book, Author author);
    }

    public class SampleService : ISampleService
    {
        private readonly IRepository _repository;
        public const decimal price = 100m;

        public SampleService(IRepository repository)
        {
            _repository = repository;
        }

        public Book UpdateBook(Book book, Author author)
        {
            book.AddAuthor(author);
            book.Price = price;
            _repository.Save(book);
            return book;
        }
    }
}