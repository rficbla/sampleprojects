using System;
using NHibernate;
using NHibernateSampleApplication.Domain;
using NHibernateSampleApplication.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace NHibernateSampleApplication.Tests.Repository
{
    public class RepositoryTests
    {
        public class RepositoryTestsBase
        {
            protected ISession _session;
            protected ISessionContainer sessionContainer;
            protected IRepository _repository;
            protected Book _book;

            [SetUp]
            public void SetUp()
            {
                _session = MockRepository.GenerateStub<ISession>();
                sessionContainer = MockRepository.GenerateMock<ISessionContainer>();
                sessionContainer.Stub(x => x.CurrentSession).Return(_session);

                _repository = new NHibernateSampleApplication.Repository.Repository(sessionContainer);
                _book = new Book();
            }
        }

        [TestFixture]
        public class When_asked_to_save_an_entity : RepositoryTestsBase
        {
            [Test]
            public void Should_call_the_SaveOrUpdate_on_CurrentSession()
            {
                _repository.Save(_book);
                sessionContainer.CurrentSession.AssertWasCalled(x => x.SaveOrUpdate(_book));
            }
        }

        [TestFixture]
        public class When_asked_to_get_an_entity : RepositoryTestsBase
        {
            [Test]
            public void Should_get_the_persisted_entity_for_the_given_id()
            {
                var id = Guid.NewGuid();
                _repository.Get<Book>(id);
                sessionContainer.CurrentSession.AssertWasCalled(x => x.Load<Book>(id));
            }
        }
    }
}