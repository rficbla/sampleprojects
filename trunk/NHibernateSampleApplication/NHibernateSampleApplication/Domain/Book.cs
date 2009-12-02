using System.Collections.Generic;

namespace NHibernateSampleApplication.Domain
{
    public class Book : DomainEntity<Book>
    {
        public Book()
        {
            Authors = new List<Author>();
        }

        public virtual string ISBN { get; set; }

        public virtual string Title { get; set; }

        public virtual decimal Price { get; set; }

        public virtual IList<Author> Authors { get; private set; }

        public virtual void AddAuthor(Author author)
        {
            if (!Authors.Contains(author))
            {
                author.Books.Add(this);
                Authors.Add(author);
            }
        }
    }
}