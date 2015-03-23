using System.Collections.Concurrent;
using System.Linq;

namespace DavidKiff.VendingMachine.UI.Domain
{
    internal sealed class VendingMachine
    {
        private readonly ConcurrentQueue<SoftDrink> softDrinks;

        public VendingMachine()
        {
            this.softDrinks = new ConcurrentQueue<SoftDrink>(Enumerable.Range(0, 25).Select(_ => new SoftDrink()));
        }

        public BuySoftDrinkResult BuyCan(CashCard cashCard)
        {
            DebitResult debitResult;
            using (var transaction = cashCard.TryDebit(SoftDrink.Cost, out debitResult))
            {
                if (debitResult.IsDebited)
                {
                    SoftDrink softDrink;
                    if (this.softDrinks.TryDequeue(out softDrink))
                    {
                        transaction.Commit();
                        return new BuySoftDrinkResult(true, softDrink);
                    }

                    return new BuySoftDrinkResult(false, errorMessage: "No soft drinks available.");
                }

                return new BuySoftDrinkResult(false, errorMessage: debitResult.DebitFailedReason);
            }
        }
    }
}