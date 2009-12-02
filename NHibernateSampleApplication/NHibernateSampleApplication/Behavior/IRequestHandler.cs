using System;

namespace NHibernateSampleApplication.Behavior
{
    public interface IRequestHandler
    {
        TOutput Invoke<TInput, TOutput>(TInput input, Func<TInput, TOutput> func)
            where TInput : class
            where TOutput : class;

        TOutput Invoke<TInput1, TInput2, TOutput>(TInput1 input1, TInput2 input2, Func<TInput1, TInput2, TOutput> func)
            where TInput1 : class
            where TInput2 : class
            where TOutput : class;
    }
}