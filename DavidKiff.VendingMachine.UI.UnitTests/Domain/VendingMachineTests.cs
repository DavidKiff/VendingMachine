using DavidKiff.VendingMachine.UI.Domain;
using NUnit.Framework;

namespace DavidKiff.VendingMachine.UI.UnitTests.Domain
{
    [TestFixture]
    internal sealed class VendingMachineTests
    {
        private UI.Domain.VendingMachine vendingMachine;

        [SetUp]
        public void TestSetup()
        {
            this.vendingMachine = new UI.Domain.VendingMachine();
        }

        [Test]
        public void CantVendMoreThan25SoftDrinks()
        {
            const int PurchaseAttemptsCount = 300;
            var cashCard = new CashCard(50);

            for (var i = 0; i < PurchaseAttemptsCount; i++)
            {
                var softDrinkResult = this.vendingMachine.BuyCan(cashCard);

                if (i >= 25)
                {
                    Assert.IsFalse(softDrinkResult.IsPurchased, "On the purchase number " + i + ", the soft drink was purchased when it should not have been.");
                    Assert.AreEqual(softDrinkResult.ErrorMessage, "No soft drinks available.", "On the purchase number " + i + " there should have been an error message as it was not purchased.");
                    Assert.IsNull(softDrinkResult.SoftDrink, "On the purchase number " + i + " the soft drink was returned, it should not have been.");
                }
                else
                {
                    Assert.IsTrue(softDrinkResult.IsPurchased, "On the purchase number " + i + " the soft drink was NOT purchased, it should have been.");
                    Assert.IsNullOrEmpty(softDrinkResult.ErrorMessage, "On the purchase number " + i + ", the soft drink was purchased, it should not have an error message.");
                    Assert.IsNotNull(softDrinkResult.SoftDrink, "On the purchase number " + i + " the soft drink was NOT returned, it should have been.");
                }
            }
        }
    }
}