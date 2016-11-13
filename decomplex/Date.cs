using System;

namespace decomplex
{
    public struct Date
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        public Date(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
        }

        public override int GetHashCode()
        {
            return Year.GetHashCode() ^ Month.GetHashCode() ^ Day.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Date)) return false;

            var date = (Date)obj;
            return date.Year == Year && date.Month == Month && date.Day == Day;
        }

        public static implicit operator DateTime(Date date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static implicit operator Date(DateTime dateTime)
        {
            return new Date(dateTime);
        }

        public override string ToString()
        {
            // TODO: Should be formated like DateTime - according to current culture. 
            return $"{Day}/{Month}/{Year}";
        }
    }
}