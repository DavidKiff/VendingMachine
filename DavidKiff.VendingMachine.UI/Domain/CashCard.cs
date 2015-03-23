using System.Threading;
using DavidKiff.VendingMachine.UI.Extensions;

namespace DavidKiff.VendingMachine.UI.Domain
{
    internal sealed class CashCard
    {
        private readonly ReaderWriterLockSlim balanceLock;
        private decimal balance;

        public CashCard(decimal initialBalanceInPounds)
        {
            this.balance = initialBalanceInPounds;
            this.balanceLock = new ReaderWriterLockSlim();
        }

        public decimal Balance
        {
            get
            {
                using (this.balanceLock.ReadLock())
                {
                    return this.balance;
                }
            }
        }

        public Transaction TryDebit(decimal amount, out DebitResult debitResult)
        {
            using (this.balanceLock.WriteLock())
            {
                var newBalance = this.balance - amount;

                const decimal MinimumAmountOnCard = 0.50M;

                var isAbleToDebit = newBalance >= MinimumAmountOnCard;

                var debitFailureReason = isAbleToDebit 
                                            ? string.Empty
                                            : string.Format("Unable to debit card as there must be a minimum balance of at least £{0:0.00} after the purchase.  You would only have £{1:0.00} left", MinimumAmountOnCard, newBalance);

                debitResult = new DebitResult(isAbleToDebit, debitFailureReason);

                if (isAbleToDebit)
                {
                    this.balance = newBalance;
                    return new Transaction(() =>
                    {
                        using (this.balanceLock.WriteLock())
                        {
                            this.balance += amount;
                        }
                    });
                }
                return Transaction.EmptyTransaction;
            }
        }
    }
}