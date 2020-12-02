using System;

namespace TollFeeCalculator.Repositories.HandleInput
{
    public interface IHandleInput
    {
        public int RunTextFile(String inputFile);
        public DateTime[] PrepareData(string inputFile);
        public string[] SplitInData(string indata);
        public DateTime[] ParseDates(string[] dateStrings);
    }
}
