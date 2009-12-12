namespace Alsing.Transactional
{
    using System;
    using System.Transactions;

    public static class OnTransactionComplete
    {
        public static void Invoke(Action action)
        {
            Transaction transaction = Transaction.Current;
            if (transaction == null)
            {
                throw new InvalidOperationException("No active transaction in scope");
            }

            //ignore transaction status
            transaction.TransactionCompleted += (s, e) => action();
        }
    }
}