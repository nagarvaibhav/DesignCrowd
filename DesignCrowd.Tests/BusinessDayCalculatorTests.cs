using DesignCrowd.HolidayRules;
using DesignCrowd.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DesignCrowd.Tests
{
    [TestClass]
    public class BusinessDayCalculatorTests
    {
        private IHolidayService _holidayService;
        private List<IHolidayRule> _rules;

        [TestInitialize]
        public void Setup()
        {
            _rules = new List<IHolidayRule>
            {
               new CertainDayHoliday(),
               new SameDayHoliday(),
               new SameDayExceptWeekendHoliday()
            };
            _holidayService = new HolidayService(_rules);
        }

        [TestMethod]
        public void WeekdaysBetweenTwoDates_TwoDatesWithNoWeekendsInBetween_ShouldReturnOne()
        {
            // arrange
            var expected = 1;
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);

            // act
            var actual = new BusinessDayCalculator(_holidayService).WeekdaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeekdaysBetweenTwoDates_TwoDatesWithWeekendsInBetween_ShouldIgnoreWeekendDaysInCount()
        {
            // arrange
            var expected = 5;
            var firstDate = new DateTime(2013, 10, 5);
            var secondDate = new DateTime(2013, 10, 14);

            // act
            var actual = new BusinessDayCalculator(_holidayService).WeekdaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeekdaysBetweenTwoDates_TwoDifferentMonthDates_ShouldIgnoreWeekendDaysInCount()
        {
            // arrange
            var expected = 61;
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);

            // act
            var actual = new BusinessDayCalculator(_holidayService).WeekdaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeekdaysBetweenTwoDates_SecondDateGreaterThanFirst_ShouldReturnZero()
        {
            // arrange
            var expected = 0;
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 5);

            // act
            var actual = new BusinessDayCalculator(_holidayService).WeekdaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WeekdaysBetweenTwoDates_TwoEqualDates_ShouldReturnZero()
        {
            // arrange
            var expected = 0;
            var firstDate = new DateTime(2013, 10, 5);
            var secondDate = new DateTime(2013, 10, 5);

            // act
            var actual = new BusinessDayCalculator(_holidayService).WeekdaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BusinessDaysBetweenTwoDates_TwoDatesNoHolidaysWithNoWeekendsInBetween_ShouldReturnOne()
        {
            // arrange
            var expected = 1;
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var holidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            // act
            var actual = new BusinessDayCalculator(_holidayService).BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BusinessDaysBetweenTwoDates_TwoDatesWithHolidaysWithNoWeekendsInBetween_ShouldReturnZero()
        {
            // arrange
            var expected = 0;
            var firstDate = new DateTime(2013, 12, 24);
            var secondDate = new DateTime(2013, 12, 27);
            var holidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            // act
            var actual = new BusinessDayCalculator(_holidayService).BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BusinessDaysBetweenTwoDates_TwoDatesWithHolidaysWithWeekendsInBetween_ShouldReturn59()
        {
            // arrange
            var expected = 59;
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var holidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            // act
            var actual = new BusinessDayCalculator(_holidayService).BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);

            // assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void BusinessDaysBetweenTwoDatesByRules_TwoDatesWithSameDayHolidayRules_ShouldReturnOne()
        {
            // arrange
            var expected = 1;
            var firstDate = new DateTime(2021, 4, 23);
            var secondDate = new DateTime(2021, 4, 27);

            // act
            var actual = new BusinessDayCalculator(_holidayService).BusinessDaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BusinessDaysBetweenTwoDatesByRules_TwoDatesWithSameDayAndSameDayExceptWeekendRules_ShouldReturnOne()
        {
            // arrange
            var expected = 1;
            var firstDate = new DateTime(2021, 12, 23);
            var secondDate = new DateTime(2021, 12, 29);
            // act
            var actual = new BusinessDayCalculator(_holidayService).BusinessDaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BusinessDaysBetweenTwoDatesByRules_QueenBirthdayDate_ShouldReturnTwo()
        {
            // arrange
            var expected = 2;
            var firstDate = new DateTime(2021, 6, 3);
            var secondDate = new DateTime(2021, 6, 9);

            // act
            var actual = new BusinessDayCalculator(_holidayService).BusinessDaysBetweenTwoDates(firstDate, secondDate);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
