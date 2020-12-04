using System;
using System.Linq;
using TollFeeCalculator.Repositories.CalculateTollFees;


namespace TollFeeCalculator.Repositories.HandleInput
{
    public class HandleInput 
    {
        private readonly ICalculateTollFees _calculateTollFees;
        public DateTime[] dates;

        public HandleInput(ICalculateTollFees calculateTollFees)
        {
            _calculateTollFees = calculateTollFees;
        }
        public int RunTextFile(string inputFile)
        {
            DateTime[] dates = PrepareData(inputFile);
            dates = SortData(dates);
            return _calculateTollFees.TotalFeeCost(dates);
        }

        public DateTime[] SortData(DateTime[] dates)
        {
            return dates.OrderBy(x => x).ToArray();          
        }

        public DateTime[] PrepareData(string inputFile)
        {
            string indata = System.IO.File.ReadAllText(inputFile);
            string[] dateStrings = SplitInData(indata);
           return ParseDates(dateStrings);
        }

        public string[] SplitInData(string indata)
        {
            return indata.Split(", ");
        }

        public DateTime[] ParseDates(string[] dateStrings)
        {
            dates = new DateTime[dateStrings.Length];

            for (int i = 0; i < dates.Length; i++)
            {
                try
                {
                    dates[i] = DateTime.Parse(dateStrings[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Invalid input data, please check your data: \r\n \r\n {e} \r\n \r\n");
                }
            }

            return dates;
        }
    }
}