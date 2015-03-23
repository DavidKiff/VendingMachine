using System;

namespace DavidKiff.VendingMachine.UI.Domain
{
    internal sealed class Transaction : IDisposable
    {
        private static readonly Action CachedEmptyRollbackAction = () => { };
        private readonly Action rollbackOperation;
        private bool isCommitted;

        public Transaction(Action rollbackOperation)
        {
            this.rollbackOperation = rollbackOperation;
        }

        public static Transaction EmptyTransaction
        {
            get { return new Transaction(CachedEmptyRollbackAction); }
        }

        public void Commit()
        {
            this.isCommitted = true;
        }

        public void Dispose()
        {
            if (!this.isCommitted) rollbackOperation();
        }
    }
}