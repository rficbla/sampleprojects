using System;
using NHibernateSampleApplication.Domain;

namespace NHibernateSampleApplication.Repository
{
    public interface IRepository
    {
        T Get<T>(Guid id) where T : DomainEntity<T>;
        void Save<T>(T entity) where T : DomainEntity<T>;
    }
}