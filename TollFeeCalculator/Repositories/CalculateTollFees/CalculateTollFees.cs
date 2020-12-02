using System;

namespace TollFeeCalculator.Repositories.CalculateTollFees
{
    public partial class CalculateTollFees
    {
        public int _minutesBetweenTollStops;
        public int _fee;
        private DateTime _startingInterval;

        public int TotalFeeCost(DateTime[] dates)
        {
            return ValidateToll(dates);
        }

        public int ValidateToll(DateTime[] dates)
        {
            _startingInterval = dates[0];

            foreach (var date in dates)
            {
                if (!TollFree(date) && date != dates[0] ||
                    !TollFree(date) && dates.Length == 1)
                {
                    //Skriv test
                    if (PassedTollSameDay(dates, date))
                    {
                        _minutesBetweenTollStops = CalculateTimeBetweenTollStops(date, _startingInterval);

                        if (_minutesBetweenTollStops >= LimitToJustPayOneTollFee)
                        {
                            _fee += GetTollFeePrice(date.Hour, date.Minute);
                        }
                        else
                        {
                            _fee += Math.Max(GetTollFeePrice(date.Hour, date.Minute), GetTollFeePrice(_startingInterval.Hour, _startingInterval.Minute));
                        }

                    }
                    else
                    {
                        _fee += GetTollFeePrice(date.Hour, date.Minute);
                    }
                }

                //Skriv test på starting interval
                _startingInterval = SetStartingInterval(_startingInterval, date);

                //Skriv test på max belopp
                if (MaxAmount(ref _fee)) break;
            }

            return _fee;
        }

        private bool PassedTollSameDay(DateTime[] dates, DateTime date)
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

        private static bool MaxAmount(ref int fee)
        {
            if (fee > MaxTollFeeAmount)
            {
                fee = MaxTollFeeAmount;
                return true;
            }

            return false;
        }

        private static DateTime SetStartingInterval(DateTime startingInterval, DateTime date)
        {
            if (startingInterval.Hour <= MaxNoneBilledHour && startingInterval.Minute <= MaxNoneBilledMinutes)
            {
                startingInterval = new DateTime(startingInterval.Year, startingInterval.Month, startingInterval.Day).AddHours(StartBilledHour).AddMinutes(StartBilledMinutes);
            }
            else
            {
                startingInterval = date;
            }

            return startingInterval;
        }

        public int CalculateTimeBetweenTollStops(DateTime date, DateTime startingInterval)
        {
            return ((date - startingInterval).Hours * HoursConversionToMinutesRate) + (date - startingInterval).Minutes;
        }

        public int GetTollFeePrice(int hour, int minute)
        {
            if (hour >= (int)TollFeeIntervalOne.MinHour &&
                hour <= (int)TollFeeIntervalOne.MaxHour &&
                minute >= (int)TollFeeIntervalOne.MinMinutes &&
                minute <= (int)TollFeeIntervalOne.MaxMinutes)
            {
                return (int)Price.MinFee;
            }

            switch (hour)
            {
                case (int)TollFeeIntervalTwo.Hour when minute >= (int)TollFeeIntervalTwo.MinMinutes && minute <= (int)TollFeeIntervalTwo.MaxMinutes:
                    return (int)Price.MinFee;
                case (int)TollFeeIntervalThree.Hour when minute >= (int)TollFeeIntervalThree.MinMinutes && minute <= (int)TollFeeIntervalThree.MaxMinutes:
                    return (int)Price.MidFee;
                case (int)TollFeeIntervalFour.Hour when minute >= (int)TollFeeIntervalFour.MinMinutes && minute <= (int)TollFeeIntervalFour.MaxMinutes:
                    return (int)Price.MaxFee;
                case (int)TollFeeIntervalFive.Hour when minute >= (int)TollFeeIntervalFive.MinMinutes && minute <= (int)TollFeeIntervalFive.MaxMinutes:
                    return (int)Price.MidFee;
            }

            switch (hour)
            {
                case (int)TollFeeIntervalSix.Hour when minute >= (int)TollFeeIntervalSix.MinMinutes && minute <= (int)TollFeeIntervalSix.MinMinutes:
                    return (int)Price.MidFee;
                case (int)TollFeeIntervalSeven.MinHour when minute >= (int)TollFeeIntervalSeven.MinMinutes:
                case (int)TollFeeIntervalSeven.MaxHour when minute <= (int)TollFeeIntervalSeven.MaxMinutes:
                    return (int)Price.MaxFee;
                case (int)TollFeeIntervalEight.Hour when minute >= (int)TollFeeIntervalEight.MinMinutes && minute <= (int)TollFeeIntervalEight.MaxMinutes:
                    return (int)Price.MidFee;
                case (int)TollFeeIntervalNine.Hour when minute >= (int)TollFeeIntervalNine.MinMinutes && minute <= (int)TollFeeIntervalNine.MaxMinutes:
                    return (int)Price.MinFee;
                default:
                    return (int)Price.Free;
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