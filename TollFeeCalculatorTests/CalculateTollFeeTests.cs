using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using TollFeeCalculator.Repositories.CalculateTollFees;
using TollFeeCalculator.Repositories.HandleInput;

namespace TollFeeCalculator.Tests
{
    [TestClass()]
    public class CalculateTollFeeTests 
    {
        [TestMethod()]
        public void ParseDates_Should_Always_Include_All_Input()
        {
            //Arrange
            var sut = new HandleInput(new CalculateTollFees());
            var input = new string[2] { "2020-06-30 00:05", "2020-06-30 06:34" };
            //Act
            var actual = sut.ParseDates(input);
            //Assert
            sut.dates.Should().HaveCount(input.Length);
        }

        [TestMethod()]
        public void If_TollFree_is_valid___fee_should_be_null()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var input = new DateTime[]
            {
                new DateTime(2020, 7, 10),
                new DateTime(2020, 4, 1).AddHours(5).AddMinutes(59),
                new DateTime(2020, 3, 1).AddHours(18).AddMinutes(31),
                new DateTime(2020, 3, 7).AddHours(6).AddMinutes(15),
                new DateTime(2020, 3, 6).AddHours(6).AddMinutes(15)

            };
            //Act
            sut.ValidateToll(input);
            //Assert
            sut._fee.Should().Be(0);
        }

        [TestMethod()]
        public void If_TollFree_is_false_CalculateTimeBetweenTollStops_should_calculate_correct_differnce_inMinutes()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var input = new DateTime[]
            {
                new DateTime(2020, 4, 1).AddHours(6).AddMinutes(59),
                new DateTime(2020, 4, 1).AddHours(7).AddMinutes(59),
            };
            //Act
            var actual = sut.ValidateToll(input);
            //Assert
            sut._minutesBetweenTollStops.Should().Be(60);
        }

        [TestMethod()]
        public void If__minutesBetweenTollStops_is_less_than_LimitToJustPayOneTollFee__fee_should_be_correct()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var fee = CalculateTollFees.Price.MinFee;
            var input = new DateTime[]
            {
                new DateTime(2020, 4, 1).AddHours(8).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(8).AddMinutes(59),
            };
            //Act
            var actual = sut.ValidateToll(input);
            //Assert
            sut._fee.Should().Be((int)fee);
        }

        [TestMethod()]
        public void If_Input_in_Validate_Toll_Has_Length_of_one_and_TollFree_is_false_Fee_should_be_correct()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var fee = CalculateTollFees.Price.MaxFee;
            var input = new DateTime[]
            {
                new DateTime(2020, 4, 1).AddHours(7).AddMinutes(31),
            };
            //Act
            var actual = sut.ValidateToll(input);
            //Assert
            sut._fee.Should().Be((int)fee);
        }
        [TestMethod()]
        public void If__fee_is_equal_or_greater_than_Max_Amount__fee_should_be_equal_to_max_amount()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var input = new DateTime[]
            {
                new DateTime(2020, 4, 1).AddHours(7).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(8).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(9).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(10).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(11).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(12).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(13).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(14).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(15).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(16).AddMinutes(31),
                new DateTime(2020, 4, 1).AddHours(17).AddMinutes(31),


            };
            //Act
            var actual = sut.ValidateToll(input);
            //Assert
            sut._fee.Should().Be(CalculateTollFees.MaxTollFeeAmount);
        }

        [TestMethod()]
        public void If_startingInterval_and_dates_have_the_same_year_month_and_day_PassedTollSameDay_should_return_true()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var date = new DateTime(2020, 4, 1).AddHours(7).AddMinutes(31);
            var startingInterval = new DateTime(2020, 4, 1).AddHours(9).AddMinutes(31);

            //Act
            var actual = sut.PassedTollSameDay(date, startingInterval);
            //Assert
            actual.Should().Be(true);
        }
        [TestMethod()]
        public void If_startingInterval_and_dates_does_not_have_the_same_year_month_and_day_PassedTollSameDay_should_return_false()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var date = new DateTime(2020, 4, 1).AddHours(7).AddMinutes(31);
            var startingInterval = new DateTime(2020, 4, 2).AddHours(9).AddMinutes(31);

            //Act
            var actual = sut.PassedTollSameDay(date, startingInterval);
            //Assert
            actual.Should().Be(false);
        }
        [TestMethod()]
        public void SetStartingInterval_should_Change_to_starTimeForCharge_if_startInterval_is_not_within_chargeble_hour_and_minutes()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var date = new DateTime(2020, 4, 1).AddHours(7).AddMinutes(31);
            var startingInterval = new DateTime(2020, 4, 2).AddHours(5).AddMinutes(0);

            //Act
            var actual = sut.SetStartingInterval(startingInterval, date);
            //Assert
            actual.Should().Be(CalculateTollFees.startTimeForCharge);
        }

        [TestMethod()]
        public void SetStartingInterval_should_Change_to_date_if_startInterval_is_within_chargeble_hour_and_minutes()
        {
            //Arrange
            var sut = new CalculateTollFees();
            var date = new DateTime(2020, 4, 1).AddHours(7).AddMinutes(31);
            var startingInterval = new DateTime(2020, 4, 2).AddHours(6).AddMinutes(42);

            //Act
            var actual = sut.SetStartingInterval(startingInterval, date);
            //Assert
            actual.Should().Be(date);
        }
    }
}