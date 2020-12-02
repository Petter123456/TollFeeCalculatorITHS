using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace TollFeeCalculator.Tests
{
    [TestClass()]
    public class CalculateTollFeeTests
    {
        [TestMethod()]
        public void SplitIndata_should_return_correct_type_and_number_of_strings()
        {
            //Arrange
            var sut = new TollFees(); 
            var input = "2020-06-30 00:05, 2020-06-30 06:34";
            //Act
            var actual = sut.SplitInData(input);
            //Assert
            actual.Should().HaveCount(2);
            actual.Should().BeOfType<string[]>(); 
        }

        [TestMethod()]
        public void ParseDates_should_return_correct_type_and_number_of_strings()
        {
            //Arrange
            var sut = new TollFees();
            string[] input = new string[]{ "2020-06-30 00:05", "2020 - 06 - 30 06:34"};
            //Act
            var actual = sut.ParseDates(input);
            //Assert
            actual.Should().HaveCount(2);
            actual.Should().BeOfType<DateTime[]>();
        }


        [TestMethod()]
        public void PrepareData_should_return_correct_type()
        {
            //Arrange
            var sut = new TollFees();
            string file = "../../../../testData.txt";
            string path = Environment.CurrentDirectory + file;

            //Act
            var actual = sut.PrepareData(path);
            //Assert
            actual.Should().BeOfType<DateTime[]>();
        }


        [TestMethod()]
        public void TotaltFeeCost_should_return_Int_and_have_same_value_as_CalculateTollFee()
        {
            //Arrange
            var sut = new TollFees();
            string[] input = new string[] { "2020-06-30 00:05", "2020 - 06 - 30 06:34" };
            var tollCalculation = sut.TollFee(sut.ParseDates(input));
            //Act
            var actual = sut.TotalFeeCost(sut.ParseDates(input));
            //Assert
            actual.Should().BeOfType(typeof(int));
            actual.Should().Be(tollCalculation);
        }

        [TestMethod()]
        public void diffInMinuts_should_not_be_greater_than_60_if_date_substraced_by_startingInterval_converted_to_minuts_differs_to_less_than_60()
        {
            //Arrange
            var sut = new TollFees();
            var diffInMinuts = TollFees.DiffInMinutes;
            string[] input = new string[] { "2020-06-30 00:05", "2020 - 06 - 30 06:34" };

            //Act
            var actual = sut.TollFee(sut.ParseDates(input));
            //Assert
            diffInMinuts.Should().BeLessThan(61); 
        }

        [TestMethod()]
        public void CalculateTollFee_should_send_the_larger_of_number()
        {
            //Arrange
            var sut = new TollFees();
            var diffInMinuts = TollFees.DiffInMinutes;
            string[] input = new string[] { "2020-06-30 00:05", "2020-06-30 06:34" };

            //Act
            var actual = sut.TollFee(sut.ParseDates(input));
            //Assert
            diffInMinuts.Should().BeGreaterThan(60);
        }
    }
}