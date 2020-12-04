using System;
using TollFeeCalculator.Repositories.CalculateTollFees;
using TollFeeCalculator.Repositories.HandleInput;

namespace TollFeeCalculator
{
    public class Program
    {
        private const string File = "../../../../Data/testData.txt";
        private static readonly string Path = Environment.CurrentDirectory + File;
        private static int fee;

        public static void Main()
        {
            ICalculateTollFees calculateTollFees = new CalculateTollFees();
            HandleInput handleInput = new HandleInput(calculateTollFees);

            fee = handleInput.RunTextFile(Path);

            Console.Write($"The total fee for valid dates the inputfile is {fee}");
        }
    }
}
