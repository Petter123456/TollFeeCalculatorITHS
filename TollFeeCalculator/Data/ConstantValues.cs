namespace TollFeeCalculator.Data
{
    public class ConstantValues
    {
        public const int LimitToJustPayOneTollFee = 60;
        public const int MaxTollFeeAmount = 60;
        public const int MaxNoneBilledHour = 6;
        public const int MaxNoneBilledMinutes = 29;
        public const int StartBilledHour = 6;
        public const int StartBilledMinutes = 30;
        public const int HoursConversionToMinutesRate = 60;
        public const int TollFreeMonth = 7;

        public enum Price
        {
            Free = 0,
            MinFee = 8,
            MidFee = 13,
            MaxFee = 18
        }

        public enum TollFeeIntervalFive
        {
            Hour = 8,
            MaxMinutes = 29,
            MinMinutes = 0
        }

        public enum TollFeeIntervalSix
        {
            Hour = 15,
            MaxMinutes = 29,
            MinMinutes = 0
        }

        public enum TollFeeIntervalOne
        {
            MinHour = 8,
            MaxHour = 14,
            MaxMinutes = 59,
            MinMinutes = 30
        }

        public enum TollFeeIntervalTwo
        {
            Hour = 6,
            MaxMinutes = 29,
            MinMinutes = 0
        }

        public enum TollFeeIntervalThree
        {
            Hour = 6,
            MaxMinutes = 59,
            MinMinutes = 30
        }

        public enum TollFeeIntervalFour
        {
            Hour = 7,
            MaxMinutes = 59,
            MinMinutes = 0
        }

        public enum TollFeeIntervalSeven
        {
            MinHour = 15,
            MaxHour = 16,
            MaxMinutes = 59,
            MinMinutes = 0
        }

        public enum TollFeeIntervalEight
        {
            Hour = 17,
            MaxMinutes = 59,
            MinMinutes = 30
        }

        public enum TollFeeIntervalNine
        {
            Hour = 18,
            MaxMinutes = 29,
            MinMinutes = 30
        }
    }
}