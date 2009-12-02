using System;
using NHibernate;

namespace NHibernateSampleApplication.Repository
{
    public class SessionContainer : ISessionContainer
    {
        private readonly ITransaction _transaction;
        private bool _isDisposed;

        public SessionContainer(ISessionFactory sessionFactory)
        {
            CurrentSession = sessionFactory.OpenSession();
            _transaction = CurrentSession.BeginTransaction();
        }

        public void Commit()
        {
            if (_isDisposed)
            {
                throw new InvalidOperationException("Could not commit as Unit of Work was not iniliazed");
            }
            _transaction.Commit();
        }

        public ISession CurrentSession { get; private set; }

        public void Dispose()
        {
            if (_isDisposed) return;
            _transaction.Dispose();
            CurrentSession.Dispose();
            _isDisposed = true;
        }
    }
}