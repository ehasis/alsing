namespace Alsing.Transactional
{
    using System;
    using System.Transactions;

    public static class OnTransactionInDoubt
    {
        public static void Invoke(Action action)
        {
            Transaction transaction = Transaction.Current;
            if (transaction == null)
            {
                throw new InvalidOperationException("No active transaction in scope");
            }

            transaction.TransactionCompleted += (s, e) =>
                                                {
                                                    if (e.Transaction.TransactionInformation.Status == TransactionStatus.InDoubt)
                                                    {
                                                        action();
                                                    }
                                                };
        }
    }
}