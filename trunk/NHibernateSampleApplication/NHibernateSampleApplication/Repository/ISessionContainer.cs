using System;
using NHibernate;

namespace NHibernateSampleApplication.Repository
{
    public interface ISessionContainer : IDisposable
    {
        void Commit();
        ISession CurrentSession { get; }
    }
}