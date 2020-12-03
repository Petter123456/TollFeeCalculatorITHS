using System;
using TollFeeCalculator.Repositories.CalculateTollFees;
using TollFeeCalculator.Repositories.HandleInput;

namespace TollFeeCalculator
{
    public class Program
    {
        private const string File = "../../../../testData.txt";
        private static readonly string Path = Environment.CurrentDirectory + File;

        public static void Main()
        {
            ICalculateTollFees calculateTollFees = new CalculateTollFees();
            HandleInput handleInput = new HandleInput(calculateTollFees);

            int fee = handleInput.RunTextFile(Path);

            Console.Write($"The total fee for the inputfile is {fee}");
        }
    }
}
