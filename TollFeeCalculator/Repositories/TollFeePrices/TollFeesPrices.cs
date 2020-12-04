using TollFeeCalculator.Data;

namespace TollFeeCalculator.Repositories.TollFeePrices
{
    public class TollFeesPrices : ConstantValues
    {
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
                case (int)TollFeeIntervalSix.Hour when minute >= (int)TollFeeIntervalSix.MinMinutes && minute <= (int)TollFeeIntervalSix.MaxMinutes:
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
    }
}