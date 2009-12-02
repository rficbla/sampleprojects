using System;
using NHibernateSampleApplication.Repository;

namespace NHibernateSampleApplication.Behavior
{
    public class AtomicRequestHandler : IRequestHandler
    {
        private readonly ISessionContainer _sessionContainer;

        public AtomicRequestHandler(ISessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;
        }

        public TOutput Invoke<TInput, TOutput>(TInput input, Func<TInput, TOutput> func)
            where TInput : class
            where TOutput : class
        {
            try
            {
                var output = func.Invoke(input);

                _sessionContainer.Commit();

                return output;
            }
            finally
            {
                _sessionContainer.Dispose();
            }
        }

        public TOutput Invoke<TInput1, TInput2, TOutput>(TInput1 input1, TInput2 input2,
                                                         Func<TInput1, TInput2, TOutput> func)
            where TInput1 : class
            where TInput2 : class
            where TOutput : class
        {
            try
            {
                var output = func.Invoke(input1, input2);

                _sessionContainer.Commit();

                return output;
            }
            finally
            {
                _sessionContainer.Dispose();
            }
        }
    }
}