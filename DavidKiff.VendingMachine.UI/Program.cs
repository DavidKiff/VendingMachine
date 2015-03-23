using System;
using DavidKiff.VendingMachine.UI.Domain;

namespace DavidKiff.VendingMachine.UI
{
    internal sealed class Program
    {
        public static void Main()
        {
            var vendingMachine = new Domain.VendingMachine();
            var cashCard = new CashCard(13.25M);

            Console.WriteLine("Welcome to the vending machine simulator!");
            Console.WriteLine("Dont forget to read the \"ReadMe.txt\"" + Environment.NewLine);

            while (true)
            {
                Console.WriteLine("Press any key to try and buy a soft drink or type \"exit\" to leave.");

                var entry = Console.ReadLine();

                if (string.Equals(entry, "exit", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }

                var result = vendingMachine.BuyCan(cashCard);

                if (result.IsPurchased)
                {
                    Console.WriteLine("You have purchased a soft drink, here it is: " + result.SoftDrink.GetHashCode());
                }
                else
                {
                    Console.WriteLine("Unable to purchase a soft drink, the error message is: " + result.ErrorMessage);
                }
            }
        }
    }
}
