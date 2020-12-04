using System;
using TollFeeCalculator.Repositories.TollFeePrices;

namespace TollFeeCalculator.Repositories.CalculateTollFees
{
    public class CalculateTollFees : TollFeesPrices, ICalculateTollFees
    {
        public int _minutesBetweenTollStops;
        public int totalFee;
        public int dailyFee;

        private DateTime _startingInterval;
        private bool validForFeeCharge;

        public static DateTime startTimeForCharge { get; private set; }

        public int TotalFeeCost(DateTime[] dates)
        {
            return ValidateToll(dates);
        }

        public int ValidateToll(DateTime[] dates)
        {
            _startingInterval = dates[0];

            foreach (var date in dates)
            {
                validForFeeCharge = ValidForFee(dates, date);

                switch (validForFeeCharge)
                {
                    case true:
                        if (PassedTollSameDay(date, _startingInterval))
                        {
                            _minutesBetweenTollStops = CalculateTimeBetweenTollStops(date, _startingInterval);

                            dailyFee += AmountAddedTDailyFee(date, _minutesBetweenTollStops);

                            MaxDailyAmount(dailyFee);                             
                        }
                        else
                        {
                            totalFee += GetTollFeePrice(date.Hour, date.Minute);
                        }
                        break;
                    default:
                        break;
                }
                _startingInterval = SetStartingInterval(_startingInterval, date);
            }

            return totalFee + dailyFee;
        }

        public bool ValidForFee(DateTime[] dates, DateTime date)
        {
            if (!TollFree(date) && date != dates[0] ||
                            !TollFree(date) && dates.Length == 1)
            {
                return true;
            }
            return false;
        }

        public bool PassedTollSameDay(DateTime date, DateTime _startingInterval)
        {
            if (date.Year == _startingInterval.Year &&
                date.Month == _startingInterval.Month &&
                date.Date == _startingInterval.Date
            )
            {
                return true;
            }

            return false;
        }
        public int CalculateTimeBetweenTollStops(DateTime date, DateTime startingInterval)
        {
            return ((date - startingInterval).Hours * HoursConversionToMinutesRate) + (date - startingInterval).Minutes;
        }

        public int AmountAddedTDailyFee(DateTime date, int _minutesBetweenTollStops)
        {
            if (_minutesBetweenTollStops >= LimitToJustPayOneTollFee)
            {
                return GetTollFeePrice(date.Hour, date.Minute);
            }
            else
            {
                return Math.Max(GetTollFeePrice(date.Hour, date.Minute), GetTollFeePrice(_startingInterval.Hour, _startingInterval.Minute));
            }
        }

        public DateTime SetStartingInterval(DateTime startingInterval, DateTime date)
        {
            if (startingInterval.Hour <= MaxNoneBilledHour && startingInterval.Minute <= MaxNoneBilledMinutes)
            {
                startTimeForCharge = new DateTime(
                    startingInterval.Year, 
                    startingInterval.Month, 
                    startingInterval.Day)
                    .AddHours(StartBilledHour)
                    .AddMinutes(StartBilledMinutes);

                startingInterval = startTimeForCharge;
            }
            else
            {
                startingInterval = date;
            }

            return startingInterval;
        }

        public void MaxDailyAmount(int fee)
        {
            if (fee >= MaxDailyTollFeeAmount)
            {
                dailyFee = MaxDailyTollFeeAmount;
            }
        }

        public bool TollFree(DateTime date)
        {
            if (date.Hour < MaxNoneBilledHour ||
                date.Hour == MaxNoneBilledHour && date.Minute <= MaxNoneBilledMinutes ||
                date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday ||
                date.Month == TollFreeMonth)
            {
                return true;
            }

            return false;
        }
    }
}