using System;
using NHibernateSampleApplication.Repository;

namespace NHibernateSampleApplication.Behavior
{
    public class TransactionBehavior : IBehavior
    {
        private readonly ITransactionManager _transactionManager;

        public TransactionBehavior(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public T2 Invoke<T1, T2>(T1 input, Func<T1, T2> func)
            where T1 : class
            where T2 : class
        {
            try
            {
                _transactionManager.Initialize();

                var output = func.Invoke(input);

                _transactionManager.Commit();

                return output;
            }
            catch
            {
                _transactionManager.Rollback();
                throw;
            }
            finally
            {
                _transactionManager.Dispose();
            }
        }
    }
}