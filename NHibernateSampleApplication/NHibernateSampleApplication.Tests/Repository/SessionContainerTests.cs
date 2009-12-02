using System;
using System.Reflection;
using FluentNHibernate;
using NHibernate;
using NHibernateSampleApplication.Repository;
using NUnit.Framework;
using Rhino.Mocks;
using FluentAssert;

namespace NHibernateSampleApplication.Tests.Repository
{
    public class SessionContainerTests
    {
        [TestFixture]
        public class When_asked_to_initialize
        {
            private ISessionFactory _sessionFactory;
            private ISession _session;
            private ITransaction _transaction;
            private ISessionContainer _sessionContainer;

            [SetUp]
            public void SetUp()
            {
                _sessionFactory = MockRepository.GenerateMock<ISessionFactory>();
                _session = MockRepository.GenerateStub<ISession>();
                _transaction = MockRepository.GenerateStub<ITransaction>();

                _sessionFactory.Expect(x => x.OpenSession()).Return(_session);
                _session.Stub(x => x.BeginTransaction()).Return(_transaction);

                _sessionContainer = new SessionContainer(_sessionFactory);
            }

            [Test]
            public void Should_initialize_the_CurrentSession()
            {
                _sessionContainer.CurrentSession.ShouldNotBeNull();
                _sessionContainer.CurrentSession.ShouldBeEqualTo(_session);
            }

            [Test]
            public void Should_begin_a_new_transaction()
            {
                _session.AssertWasCalled(x => x.BeginTransaction());
            }
        }

        [TestFixture]
        public class When_asked_to_commit
        {
            private ISessionFactory _sessionFactory;
            private ISession _session;
            private ITransaction _transaction;
            private ISessionContainer _sessionContainer;

            [SetUp]
            public void SetUp()
            {
                _sessionFactory = MockRepository.GenerateMock<ISessionFactory>();
                _session = MockRepository.GenerateStub<ISession>();
                _transaction = MockRepository.GenerateStub<ITransaction>();

                _sessionFactory.Expect(x => x.OpenSession()).Return(_session);
                _session.Stub(x => x.BeginTransaction()).Return(_transaction);

                _sessionContainer = new SessionContainer(_sessionFactory);
            }

            [Test]
            public void Should_commit_the_current_transaction()
            {
                _sessionContainer.Commit();

                _transaction.AssertWasCalled(t => t.Commit());
            }

            [Test]
            public void Should_throw_an_exception_if_the_unit_of_work_is_disposed()
            {
                FieldInfo fieldinfo = _sessionContainer.GetType().GetField("_isDisposed",
                                                                           BindingFlags.Instance |
                                                                           BindingFlags.NonPublic);
                fieldinfo.SetValue(_sessionContainer, true);

                Assert.Throws<InvalidOperationException>(() => _sessionContainer.Commit());
            }
        }

        [TestFixture]
        public class When_asked_to_dispose
        {
            private ISessionFactory _sessionFactory;
            private ISession _session;
            private ITransaction _transaction;
            private ISessionContainer _sessionContainer;

            [SetUp]
            public void SetUp()
            {
                _sessionFactory = MockRepository.GenerateMock<ISessionFactory>();
                _session = MockRepository.GenerateStub<ISession>();
                _transaction = MockRepository.GenerateStub<ITransaction>();

                _sessionFactory.Expect(x => x.OpenSession()).Return(_session);
                _session.Stub(x => x.BeginTransaction()).Return(_transaction);

                _sessionContainer = new SessionContainer(_sessionFactory);
            }

            [Test]
            public void Should_dispose_the_CurrentSession_and_transaction()
            {
                _sessionContainer.Dispose();

                _transaction.AssertWasCalled(x => x.Dispose());
                _session.AssertWasCalled(x => x.Dispose());
            }


            [Test]
            public void Should_not_dispose_the_CurrentSession_and_transaction_if_the_Session_is_already_disposed()
            {
                FieldInfo fieldinfo = _sessionContainer.GetType().GetField("_isDisposed",
                                                                           BindingFlags.Instance |
                                                                           BindingFlags.NonPublic);
                fieldinfo.SetValue(_sessionContainer, true);

                _sessionContainer.Dispose();

                _transaction.AssertWasNotCalled(x => x.Dispose());
                _session.AssertWasNotCalled(x => x.Dispose());
            }
        }
    }
}