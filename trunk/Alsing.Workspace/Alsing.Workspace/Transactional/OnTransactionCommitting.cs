namespace Alsing.Transactional
{
    using System;
    using System.Transactions;

    public static class OnTransactionCommitting
    {
        public static void Invoke(Action action)
        {
            Transaction transaction = Transaction.Current;
            if (transaction == null)
            {
                throw new InvalidOperationException("No active transaction in scope");
            }

            var handler = new OnTransactionCommittingHandler(action);
            transaction.EnlistVolatile(handler, EnlistmentOptions.None);
        }
    }

    public class OnTransactionCommittingHandler : IEnlistmentNotification
    {
        private readonly Action action;

        public OnTransactionCommittingHandler(Action action)
        {
            this.action = action;
        }

        public void Commit(Enlistment enlistment)
        {
            this.action();
        }

        public void InDoubt(Enlistment enlistment)
        {
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
        }

        public void Rollback(Enlistment enlistment)
        {
        }
    }
}