namespace DavidKiff.VendingMachine.UI.Domain
{
    internal sealed class BuySoftDrinkResult
    {
        public BuySoftDrinkResult(bool isPurchased, SoftDrink softDrink = null, string errorMessage = null)
        {
            this.IsPurchased = isPurchased;
            this.SoftDrink = softDrink;
            this.ErrorMessage = errorMessage;
        }

        public bool IsPurchased { get; private set; }

        public SoftDrink SoftDrink { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}