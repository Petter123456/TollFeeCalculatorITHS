using System;

namespace TollFeeCalculator
{
    public interface ITollFees
    {
        void RunTextFile(String inputFile);
        DateTime[] PrepareData(string inputFile);
        string[] SplitInData(string indata);
        DateTime[] ParseDates(string[] dateStrings);
        int TotalFeeCost(DateTime[] dates);
        int TollFee(DateTime[] dates);
        int CalculateTimeBetweenTollStops(DateTime date, DateTime startingInterval);
        int GetTollFeePrice(int hour, int minute);
        bool TollFree(DateTime date);
    }
}