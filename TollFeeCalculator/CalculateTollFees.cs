using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
   public class CalculateTollFees : ITollFees
    {
        public static int DiffInMinutes;
        private static readonly int LimitForDoubleToll = 60;

        public void RunTextFile(String inputFile)
        {
            DateTime[] dates = PrepareData(inputFile);
            Console.Write("The total fee for the inputfile is " + TotalFeeCost(dates));
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
            DateTime[] dates = new DateTime[dateStrings.Length];
            for (int i = 0; i < dates.Length; i++)
            {
                dates[i] = DateTime.Parse(dateStrings[i]);
            }

            return dates;
        }

        public int TotalFeeCost(DateTime[] dates)
        {
            int fee = TollFee(dates);
            return fee;
        }

        public int TollFee(DateTime[] dates)
        {
            int fee = 0;
            DateTime startingInterval = dates[0];

            foreach (var date in dates)
            {
                if (!TollFree(date))
                {
                    var minutesBetweenTollStops = CalculateTimeBetweenTollStops(date, startingInterval);

                    if (minutesBetweenTollStops >= LimitForDoubleToll)
                    {
                        fee += GetTollFeePrice(date.Hour, date.Minute);
                    }
                    else
                    {
                        fee += Math.Max(GetTollFeePrice(date.Hour, date.Minute), GetTollFeePrice(startingInterval.Hour, startingInterval.Minute));
                    }
                }

                if (startingInterval.Hour <= 6 && startingInterval.Minute <= 29)
                {
                    startingInterval = new DateTime(2020, 6, 30).AddHours(6).AddMinutes(30);
                }
                else
                {
                    startingInterval = date;

                }

                if (fee > 60)
                {
                    fee = 60;
                    break;
                }
            }

            //Skriv test på max belopp
            return fee;
        }

        public int CalculateTimeBetweenTollStops(DateTime date, DateTime startingInterval)
        {
            return ((date - startingInterval).Hours * 60) + (date - startingInterval).Minutes;
        }

        public int GetTollFeePrice(int hour, int minute)
        {
            if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59)
            {
                return 8;
            }

            switch (hour)
            {
                case 6 when minute >= 0 && minute <= 29:
                    return 8;
                case 6 when minute >= 30 && minute <= 59:
                    return 13;
                case 7 when minute >= 0 && minute <= 59:
                    return 18;
                case 8 when minute >= 0 && minute <= 29:
                    return 13;
            }

            switch (hour)
            {
                case 15 when minute >= 0 && minute <= 29:
                    return 13;
                case 15 when minute >= 0:
                case 16 when minute <= 59:
                    return 18;
                case 17 when minute >= 0 && minute <= 59:
                    return 13;
                case 18 when minute >= 0 && minute <= 29:
                    return 8;
                default:
                    return 0;
            }
        }

        public bool TollFree(DateTime date)
        {
            var hour = date.Hour;
            var minute = date.Minute;
            var dayOfWeek = date.DayOfWeek;
            var month = date.Month;

            if (hour < 6 ||
                hour == 6 && minute <= 29 ||
                dayOfWeek == DayOfWeek.Saturday ||
                dayOfWeek == DayOfWeek.Sunday ||
                month == 7)
            {
                return true;
            }

            return false;
        }
    }
}

