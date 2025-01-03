using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRML.Classes
{
    static class Helper
    {
       public static string CalculateDifference(DateTime start, DateTime end)
        {
            // Initial difference in years, months, and days
            double years = end.Year - start.Year;
            double months = end.Month - start.Month;
            double days = end.Day - start.Day;

            // Adjust if the month difference is negative
            if (months < 0)
            {
                years--;
                months += 12;
            }

            // Adjust if the day difference is negative
            if (days < 0)
            {
                months--;
                // Get the number of days in the previous month
                DateTime previousMonth = end.AddMonths(-1);
                days += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);
            }

            if (years < 1 && months < 1 && days < 1)
            {
                return "Tomorrow.";
            }
            else if (years < 1 && months < 1 && days > 0)
            {

                return "in " + days + " day(s).";
            }
            else if (years < 1 && months >= 1 && days < 1)
            {
                return "in " + months + " month(s).";
            }
            else if (years<1 && months>0 && days>0)
            {
                return "in "+months + " months & " + days + " days.";
            }
            else if (years >0 && months <1 && days <1)
            {
                return "in " + months + " Year(s) ";
            }
            else if (years > 0 && months < 1 && days >0)
            {
                return $"in {years} Year(s) and {days} day(s)";
            }
            else if (years > 0 && months >0 && days <1)
            {
                return $"in {years} Year(s) and {months} month(s)";
            }
            else
            {
                return "in " + years + " Year(s)," + months + " months & " + days + " days.";
            }
        }
    }
}
