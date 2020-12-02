using System;
using Microsoft.Extensions.DependencyInjection;

namespace TollFeeCalculator
{
    public class TollFees 
    {
        private const string File = "../../../../testData.txt";
        private static readonly string Path = Environment.CurrentDirectory + File;
        private static ITollFees _tollFees;

        public TollFees(ITollFees tollFees)
        {
            _tollFees = tollFees;
        }

        public static void Main(){

            ITollFees iTollFees = new CalculateTollFees(); 
            TollFees tollFees = new TollFees(iTollFees);
            _tollFees.RunTextFile(Path);
        }
    }
}
