using System;
using TollFeeCalculator.Repositories.CalculateTollFees;
using TollFeeCalculator.Repositories.HandleInput;

namespace TollFeeCalculator
{
    public class Program
    {
        private const string File = "../../../../testData.txt";
        private static readonly string Path = Environment.CurrentDirectory + File;
        private static HandleInput _handleInput;

        public Program(HandleInput handleInput)
        {
            _handleInput = handleInput;
        }

        public static void Main()
        {
            CalculateTollFees calculateTollFees = new CalculateTollFees();
            Program program = new Program(new HandleInput(calculateTollFees));

            var totalFee = _handleInput.RunTextFile(Path);

            Console.Write($"The total fee for the inputfile is {totalFee}");

        }
    }
}
