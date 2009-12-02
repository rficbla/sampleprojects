using System;
using NHibernateSampleApplication.Behavior;
using NHibernateSampleApplication.Domain;
using StructureMap;

namespace NHibernateSampleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BootStrapper.BootStrap();
            ISampleService sampleService = ObjectFactory.GetInstance<ISampleService>();
            IRequestHandler unitOfWork = ObjectFactory.GetInstance<IRequestHandler>();
            Book result = unitOfWork.Invoke<Book, Author, Book>(GetBook(), GetAuthor(), sampleService.UpdateBook);
            Console.WriteLine(result.Title);
        }

        public static Book GetBook()
        {
            var book = new Book
                           {
                               ISBN = "2367",
                               Title = "Test2 Title",
                               Price = 24m,
                           };
            return book;
        }

        public static Author GetAuthor()
        {
            return new Author
                       {
                           Name = "Some High Profile Author",
                       };
        }
    }
}