using System.Collections.Generic;

namespace NHibernateSampleApplication.Domain
{
    public class Author : DomainEntity<Author>
    {
        public Author()
        {
            Books = new List<Book>();
        }

        public virtual string Name { get; set; }

        public virtual IList<Book> Books { get; private set; }
    }
}