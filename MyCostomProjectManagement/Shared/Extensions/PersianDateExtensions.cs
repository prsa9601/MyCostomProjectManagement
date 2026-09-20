using System.Globalization;

namespace MyCostomProjectManagement.Shared.Extensions
{
    public static class PersianDateExtensions
    {
        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی با فرمت دلخواه
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ شمسی به صورت رشته</returns>
        public static string ToPersianDateString(this DateTime date)
        {
            var pc = new PersianCalendar();
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);
            var day = pc.GetDayOfMonth(date);

            var monthNames = new[] {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
        };

            return $"{day} {monthNames[month - 1]} {year}";
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی با فرمت عددی
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <param name="separator">جداکننده (پیشفرض: /)</param>
        /// <returns>تاریخ شمسی عددی</returns>
        public static string ToPersianDateNumber(this DateTime date, string separator = "/")
        {
            var pc = new PersianCalendar();
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);
            var day = pc.GetDayOfMonth(date);

            return $"{year}{separator}{month:D2}{separator}{day:D2}";
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی کامل با روز هفته
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ شمسی کامل</returns>
        public static string ToPersianDateFull(this DateTime date)
        {
            var pc = new PersianCalendar();
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);
            var day = pc.GetDayOfMonth(date);
            var dayOfWeek = pc.GetDayOfWeek(date);

            var monthNames = new[] {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
        };

            var dayNames = new[] {
            "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"
        };

            return $"{dayNames[(int)dayOfWeek]}، {day} {monthNames[month - 1]} {year}";
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی با ساعت
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ و زمان شمسی</returns>
        public static string ToPersianDateTime(this DateTime date)
        {
            var pc = new PersianCalendar();
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);
            var day = pc.GetDayOfMonth(date);
            var hour = date.Hour;
            var minute = date.Minute;

            var monthNames = new[] {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
        };

            return $"{day} {monthNames[month - 1]} {year}، {hour:D2}:{minute:D2}";
        }

        /// <summary>
        /// تبدیل رشته تاریخ شمسی به تاریخ میلادی
        /// </summary>
        /// <param name="persianDate">تاریخ شمسی (فرمت: 1403/01/01)</param>
        /// <returns>تاریخ میلادی</returns>
        public static DateTime PersianDateToDateTime(this string persianDate)
        {
            var parts = persianDate.Split('/');
            if (parts.Length != 3)
                throw new ArgumentException("فرمت تاریخ صحیح نیست. باید به صورت 1403/01/01 باشد.");

            var pc = new PersianCalendar();
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);

            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        /// <summary>
        /// گرفتن نام ماه شمسی
        /// </summary>
        /// <param name="monthNumber">شماره ماه (1-12)</param>
        /// <returns>نام ماه</returns>
        public static string GetPersianMonthName(int monthNumber)
        {
            if (monthNumber < 1 || monthNumber > 12)
                throw new ArgumentException("شماره ماه باید بین 1 تا 12 باشد.");

            var monthNames = new[] {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
        };

            return monthNames[monthNumber - 1];
        }

        /// <summary>
        /// گرفتن نام روز هفته
        /// </summary>
        /// <param name="date">تاریخ</param>
        /// <returns>نام روز هفته</returns>
        public static string GetPersianDayOfWeek(this DateTime date)
        {
            var pc = new PersianCalendar();
            var dayOfWeek = pc.GetDayOfWeek(date);

            var dayNames = new[] {
            "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"
        };

            return dayNames[(int)dayOfWeek];
        }

        /// <summary>
        /// بررسی اینکه آیا تاریخ شمسی معتبر است
        /// </summary>
        /// <param name="persianDate">تاریخ شمسی (فرمت: 1403/01/01)</param>
        /// <returns>آیا تاریخ معتبر است</returns>
        public static bool IsValidPersianDate(this string persianDate)
        {
            try
            {
                var parts = persianDate.Split('/');
                if (parts.Length != 3)
                    return false;

                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);

                if (year < 1 || year > 1500)
                    return false;

                if (month < 1 || month > 12)
                    return false;

                if (day < 1 || day > 31)
                    return false;

                // بررسی روزهای ماه
                var pc = new PersianCalendar();
                var daysInMonth = pc.GetDaysInMonth(year, month);

                return day <= daysInMonth;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// محاسبه سن بر اساس تاریخ تولد شمسی
        /// </summary>
        /// <param name="birthDate">تاریخ تولد شمسی (فرمت: 1370/01/01)</param>
        /// <returns>سن به سال</returns>
        public static int CalculateAgeFromPersianDate(this string birthDate)
        {
            if (!birthDate.IsValidPersianDate())
                throw new ArgumentException("تاریخ تولد نامعتبر است.");

            var birthDateTime = birthDate.PersianDateToDateTime();
            var today = DateTime.Today;
            var age = today.Year - birthDateTime.Year;

            // اگر هنوز روز تولد امسال نرسیده باشد، یک سال کم می‌کنیم
            if (birthDateTime.Date > today.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به تاریخ شمسی با فرمت قابل نمایش در UI
        /// </summary>
        /// <param name="date">تاریخ میلادی</param>
        /// <returns>تاریخ شمسی با فرمت زیبا</returns>
        public static string ToPersianDateDisplay(this DateTime date)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);
            var tomorrow = today.AddDays(1);

            if (date.Date == today)
                return "امروز";

            if (date.Date == yesterday)
                return "دیروز";

            if (date.Date == tomorrow)
                return "فردا";

            return date.ToPersianDateString();
        }

        /// <summary>
        /// گرفتن تفاوت زمانی به صورت متن فارسی
        /// </summary>
        /// <param name="date">تاریخ</param>
        /// <returns>تفاوت زمانی به فارسی</returns>
        public static string GetPersianTimeAgo(this DateTime date)
        {
            var now = DateTime.Now;
            var diff = now - date;

            if (diff.TotalDays >= 365)
            {
                var years = (int)(diff.TotalDays / 365);
                return $"{years} سال پیش";
            }

            if (diff.TotalDays >= 30)
            {
                var months = (int)(diff.TotalDays / 30);
                return $"{months} ماه پیش";
            }

            if (diff.TotalDays >= 7)
            {
                var weeks = (int)(diff.TotalDays / 7);
                return $"{weeks} هفته پیش";
            }

            if (diff.TotalDays >= 1)
            {
                var days = (int)diff.TotalDays;
                return $"{days} روز پیش";
            }

            if (diff.TotalHours >= 1)
            {
                var hours = (int)diff.TotalHours;
                return $"{hours} ساعت پیش";
            }

            if (diff.TotalMinutes >= 1)
            {
                var minutes = (int)diff.TotalMinutes;
                return $"{minutes} دقیقه پیش";
            }

            return "همین الان";
        }
    }

    // کلاس PersianDateTime برای کار راحت‌تر با تاریخ شمسی
    public class PersianDateTime
    {
        private readonly PersianCalendar _pc;
        private readonly DateTime _dateTime;

        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public DayOfWeek DayOfWeek { get; }

        public PersianDateTime(DateTime dateTime)
        {
            _pc = new PersianCalendar();
            _dateTime = dateTime;

            Year = _pc.GetYear(dateTime);
            Month = _pc.GetMonth(dateTime);
            Day = _pc.GetDayOfMonth(dateTime);
            Hour = _pc.GetHour(dateTime);
            Minute = _pc.GetMinute(dateTime);
            Second = _pc.GetSecond(dateTime);
            DayOfWeek = _pc.GetDayOfWeek(dateTime);
        }

        public PersianDateTime(int year, int month, int day)
        {
            _pc = new PersianCalendar();
            _dateTime = _pc.ToDateTime(year, month, day, 0, 0, 0, 0);

            Year = year;
            Month = month;
            Day = day;
            Hour = 0;
            Minute = 0;
            Second = 0;
            DayOfWeek = _pc.GetDayOfWeek(_dateTime);
        }

        public static PersianDateTime Now => new(DateTime.Now);
        public static PersianDateTime Today => new(DateTime.Today);

        public DateTime ToDateTime() => _dateTime;

        public string ToString(string format)
        {
            return format switch
            {
                "d" => $"{Year}/{Month:D2}/{Day:D2}",
                "D" => $"{Day} {GetPersianMonthName()} {Year}",
                "f" => $"{GetPersianDayOfWeekName()}، {Day} {GetPersianMonthName()} {Year}",
                "g" => $"{Year}/{Month:D2}/{Day:D2} {Hour:D2}:{Minute:D2}",
                "G" => $"{Year}/{Month:D2}/{Day:D2} {Hour:D2}:{Minute:D2}:{Second:D2}",
                "t" => $"{Hour:D2}:{Minute:D2}",
                "T" => $"{Hour:D2}:{Minute:D2}:{Second:D2}",
                _ => $"{Year}/{Month:D2}/{Day:D2}"
            };
        }

        public string GetPersianMonthName() => PersianDateExtensions.GetPersianMonthName(Month);

        public string GetPersianDayOfWeekName()
        {
            var dayNames = new[] {
            "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"
        };

            return dayNames[(int)DayOfWeek];
        }

        public PersianDateTime AddDays(int days) => new(_dateTime.AddDays(days));
        public PersianDateTime AddMonths(int months) => new(_dateTime.AddMonths(months));
        public PersianDateTime AddYears(int years) => new(_dateTime.AddYears(years));

        public override string ToString() => ToString("d");
    }

    // کلاس برای استفاده در Blazor Components
    public static class PersianDateHelper
    {
        private static readonly PersianCalendar _pc = new();

        /// <summary>
        /// نمایش تاریخ جاری به شمسی در فرمت کامل
        /// </summary>
        public static string CurrentPersianDate => DateTime.Now.ToPersianDateFull();

        /// <summary>
        /// نمایش تاریخ جاری به شمسی در فرمت عددی
        /// </summary>
        public static string CurrentPersianDateNumber => DateTime.Now.ToPersianDateNumber();

        /// <summary>
        /// گرفتن لیست ماه‌های شمسی
        /// </summary>
        public static string[] PersianMonths => new[] {
        "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
        "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
    };

        /// <summary>
        /// گرفتن لیست روزهای هفته
        /// </summary>
        public static string[] PersianDaysOfWeek => new[] {
        "شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه"
    };

        /// <summary>
        /// تولید لیست سال‌های شمسی (از 1300 تا امسال + 10 سال آینده)
        /// </summary>
        public static List<int> GetPersianYears()
        {
            var currentYear = _pc.GetYear(DateTime.Now);
            var years = new List<int>();

            for (int year = 1300; year <= currentYear + 10; year++)
            {
                years.Add(year);
            }

            return years;
        }

        /// <summary>
        /// تولید لیست روزهای ماه شمسی
        /// </summary>
        /// <param name="year">سال شمسی</param>
        /// <param name="month">ماه شمسی</param>
        /// <returns>لیست روزها</returns>
        public static List<int> GetPersianMonthDays(int year, int month)
        {
            var daysInMonth = _pc.GetDaysInMonth(year, month);
            var days = new List<int>();

            for (int day = 1; day <= daysInMonth; day++)
            {
                days.Add(day);
            }

            return days;
        }
    }
}

