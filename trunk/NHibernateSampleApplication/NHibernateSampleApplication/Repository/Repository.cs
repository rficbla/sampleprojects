using System;
using NHibernateSampleApplication.Domain;

namespace NHibernateSampleApplication.Repository
{
    public class Repository : IRepository
    {
        private readonly ISessionContainer sessionContainer;

        public Repository(ISessionContainer sessionContainer)
        {
            this.sessionContainer = sessionContainer;
        }

        public T Get<T>(Guid id) where T : DomainEntity<T>
        {
            return sessionContainer.CurrentSession.Load<T>(id);
        }

        public void Save<T>(T entity) where T : DomainEntity<T>
        {
            sessionContainer.CurrentSession.SaveOrUpdate(entity);
        }
    }
}