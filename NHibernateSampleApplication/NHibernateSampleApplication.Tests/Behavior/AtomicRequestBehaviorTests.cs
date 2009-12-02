using System;
using NHibernateSampleApplication.Behavior;
using NHibernateSampleApplication.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace NHibernateSampleApplication.Tests.Behavior
{
    public class AtomicRequestBehaviorTests
    {
        public class InputEntity1
        {
        }

        public class InputEntity2
        {
        }

        [TestFixture]
        public class When_asked_to_execute_an_action_within_a_Request_for_one_input_entity
        {
            private ISessionContainer sessionContainer;
            private IRequestHandler _atomicRequestHandler;

            [SetUp]
            public void SetUp()
            {
                sessionContainer = MockRepository.GenerateMock<ISessionContainer>();
                _atomicRequestHandler = new AtomicRequestHandler(sessionContainer);
            }

            public InputEntity1 Success(InputEntity1 entity1)
            {
                return entity1;
            }

            public InputEntity1 Failure(InputEntity1 entity1)
            {
                throw new Exception();
            }

            [Test]
            public void Should_commit_if_the_action_was_success()
            {
                InputEntity1 input = new InputEntity1();
                _atomicRequestHandler.Invoke<InputEntity1, InputEntity1>(input, Success);

                sessionContainer.AssertWasCalled(x => x.Commit());
                sessionContainer.AssertWasCalled(x => x.Dispose());
            }

            [Test]
            public void Should_dispose_and_throw_the_generated_exception_if_the_action_fails()
            {
                InputEntity1 input = new InputEntity1();

                Assert.Throws<Exception>(() => _atomicRequestHandler.Invoke<InputEntity1, InputEntity1>(input, Failure));
                sessionContainer.AssertWasNotCalled(x => x.Commit());
                sessionContainer.AssertWasCalled(x => x.Dispose());
            }
        }

        public class When_asked_to_execute_an_action_within_a_Request_for_two_input_entities
        {
            private ISessionContainer sessionContainer;
            private IRequestHandler _atomicRequestHandler;

            [SetUp]
            public void SetUp()
            {
                sessionContainer = MockRepository.GenerateMock<ISessionContainer>();
                _atomicRequestHandler = new AtomicRequestHandler(sessionContainer);
            }

            public InputEntity1 Success(InputEntity1 entity1, InputEntity2 entity2)
            {
                return entity1;
            }

            public InputEntity1 Failure(InputEntity1 entity1, InputEntity2 entity2)
            {
                throw new Exception();
            }

            [Test]
            public void Should_commit_if_the_action_was_success()
            {
                InputEntity1 input1 = new InputEntity1();
                InputEntity2 input2 = new InputEntity2();
                _atomicRequestHandler.Invoke<InputEntity1, InputEntity2, InputEntity1>(input1, input2, Success);

                sessionContainer.AssertWasCalled(x => x.Commit());
                sessionContainer.AssertWasCalled(x => x.Dispose());
            }

            [Test]
            public void Should_dispose_and_throw_the_generated_exception_if_the_action_fails()
            {
                InputEntity1 input1 = new InputEntity1();
                InputEntity2 input2 = new InputEntity2();

                Assert.Throws<Exception>(
                    () => _atomicRequestHandler.Invoke<InputEntity1, InputEntity2, InputEntity1>(input1, input2, Failure));
                sessionContainer.AssertWasNotCalled(x => x.Commit());
                sessionContainer.AssertWasCalled(x => x.Dispose());
            }
        }
    }
}