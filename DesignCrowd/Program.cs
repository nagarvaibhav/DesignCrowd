using DesignCrowd.HolidayRules;
using DesignCrowd.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DesignCrowd
{
    class Program
    {
        static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(environment))
            {
                environment = "Development";
            }
            var configBuilder = new ConfigurationBuilder();

            IConfiguration config = configBuilder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IHolidayRule, CertainDayHoliday>()
                .AddSingleton<IHolidayRule, SameDayExceptWeekendHoliday>()
                .AddSingleton<IHolidayRule, SameDayHoliday>()
                .AddSingleton<IHolidayService, HolidayService>()
                .BuildServiceProvider();

            var holidayService = serviceProvider.GetService<IHolidayService>();
            Console.WriteLine("Enter First Date in dd/MM/yyyy");
            var firstDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
            Console.WriteLine("Enter Last Date in dd/MM/yyyy");
            var lastDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);

            var dayCalculator = new BusinessDayCalculator(holidayService);

            var countWeekDays = dayCalculator.WeekdaysBetweenTwoDates(firstDate, lastDate);
            Console.WriteLine($"Excercise 1 : Weekdays Count between the dates are: {countWeekDays}");

            var holidays = new List<DateTime>
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };

            var countWeekDaysFromHoliday = dayCalculator.BusinessDaysBetweenTwoDates(firstDate, lastDate, holidays);
            Console.WriteLine($"Excercise 2 : Business Days Count based on public holidays between the dates are: {countWeekDaysFromHoliday}");

            var count = dayCalculator.BusinessDaysBetweenTwoDates(firstDate, lastDate);
            Console.WriteLine($"Excercise 3 : Business Days Count based on rules betwwen the dates are: {count}");

            Console.ReadKey();

        }
    }
}
