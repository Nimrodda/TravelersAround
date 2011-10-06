using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TravelersAround.ServiceProxy
{
    [SplitDate(ErrorMessage = "Invalid date")]
    public class BirthdayPicker
    {
        [Required]
        public int Day { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }

        public BirthdayPicker()
        {
        }

        public BirthdayPicker(string date)
        {
            DateTime temp;
            if (DateTime.TryParse(date, out temp))
            {
                Day = temp.Day;
                Month = temp.Month;
                Year = temp.Year;
            }
        }

        public BirthdayPicker(DateTime date)
        {
                Day = date.Day;
                Month = date.Month;
                Year = date.Year;
        }

        public override string ToString()
        {
            return String.Concat(Day, "/", Month, "/", Year);
        }
    }

    public class SplitDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                BirthdayPicker objValue = (BirthdayPicker)value;
                DateTime birthday = new DateTime(objValue.Year, objValue.Month, objValue.Day);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
