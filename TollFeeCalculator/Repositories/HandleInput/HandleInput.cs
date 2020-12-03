using System;
using System.Globalization;
using System.Linq;

namespace TollFeeCalculator.Repositories.HandleInput
{
    public class HandleInput
    {
        private readonly CalculateTollFees.CalculateTollFees _calculateTollFees;
        public DateTime[] dates;

        public HandleInput(CalculateTollFees.CalculateTollFees calculateTollFees)
        {
            _calculateTollFees = calculateTollFees;
        }
        public int RunTextFile(string inputFile)
        {
            DateTime[] dates = PrepareData(inputFile);
            dates = SortData(dates);
            return _calculateTollFees.TotalFeeCost(dates);
        }

        //Skriv Test på sort data 
        private DateTime[] SortData(DateTime[] dates)
        {
            return dates.OrderBy(x => x.Year).ToArray();
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
                dates[i] = DateTime.Parse(dateStrings[i]);
            }

            return dates;
        }
    }
}