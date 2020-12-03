using System;

namespace TollFeeCalculator.Repositories.CalculateTollFees
{
    public interface ICalculateTollFees
    {
        int TotalFeeCost(DateTime[] dates);
        int CalculateTimeBetweenTollStops(DateTime date, DateTime startingInterval);
        int GetTollFeePrice(int hour, int minute);
        bool TollFree(DateTime date);
    }
}