using DavidKiff.VendingMachine.UI.Domain;
using NUnit.Framework;

namespace DavidKiff.VendingMachine.UI.UnitTests.Domain
{
    [TestFixture]
    internal sealed class CashCardTests
    {
        [Test]
        [TestCase(0.05, false)]
        [TestCase(0.40, false)]
        [TestCase(0.49, false)]
        [TestCase(0.50, true)]
        [TestCase(0.51, true)]
        [TestCase(0.60, true)]
        public void CantVendIfLessThan50pOnTheCard(decimal cardBalance, bool canVend)
        {
            var cashCard = new CashCard(cardBalance);

            DebitResult debitResult;
            cashCard.TryDebit(0.00M, out debitResult);

            Assert.AreEqual(canVend, debitResult.IsDebited, "We were " + (canVend ? "NOT " : string.Empty) + "able to vend with a balance of " + cardBalance);
        }

        [Test]
        [TestCase(10.50, 89.50)]
        [TestCase(20.519, 79.481)]
        [TestCase(50, 50)]
        [TestCase(99.51, 100)]
        [TestCase(99.50000000001, 100)]
        public void CashCardBalanceIsDecrementedOnDebit(decimal amountToDebit, decimal expectedBalance)
        {
            const decimal InitialBalance = 100;

            var cashCard = new CashCard(InitialBalance);

            DebitResult debitResult;
            using (var transaction = cashCard.TryDebit(amountToDebit, out debitResult))
            {
                if (debitResult.IsDebited) transaction.Commit();
            }

            Assert.AreEqual(expectedBalance, cashCard.Balance, string.Format("Debit £{0} from £{1} should result in a balance of £{2}.", amountToDebit, InitialBalance, expectedBalance));
        }
    }
}